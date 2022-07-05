using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectClient
{
    public partial class Form1 : Form
    {

        string username = "", userToBlock = "";
        bool isServerDown = false;
        bool isThreadCalled = false;

        bool terminating = false;
        bool connected = false;
        bool serverCrash = true;
        Socket clientSocket;

        public Form1()
        {

            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_sendUsername_Click(object sender, EventArgs e)
        {
            try
            {
                username = textBox_username.Text;
                if(username == "")
                {
                    richTextBox1.AppendText("Please provide a username.\n");
                }
                if (username != "" && username.Length <= 60)
                {
                    
                    string userinfo = "UD:" + username;
                    sendMessage(ref clientSocket, userinfo, 64);
                }
                else
                {
                    richTextBoxFeed.AppendText("Username can not be empty or longer than 60 characters.\n");
                }

            }
            catch
            {
                if (!terminating)
                {
                    if(serverCrash)
                    {
                        richTextBox1.AppendText("The server has disconnected.\n");
                    }
                    setUItoNotConnected();

                }

                clientSocket.Close();
                connected = false;
            }
        }

        private void setUItoNotConnected()
        {
            button_disconnect.Enabled = false;
            button_sendSweet.Enabled = false;
            button_sendUsername.Enabled = false;
            button_requestAllSweets.Enabled = false;
            textBox_username.Enabled = false;
            button_connect.Enabled = true;
            textBox_enterSweet.Enabled = false;
            button_reqUsername.Enabled = false;
            buttonFollow.Enabled = false;
            textBoxFollowUser.Enabled = false;
            buttonFollowedUsersTweet.Enabled = false;
            button_FollowersReq.Enabled = false;
            button_FollowedUsers.Enabled = false;
            button_DeleteSweet.Enabled = false;
            button_block.Enabled = false;

        }


        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[1024];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    //parse message
                    string[] splittedMessage = incomingMessage.Split(':');
                    string code = splittedMessage[0];
                    string data = splittedMessage[1];


                    if (code == "DC") //disconnect
                    {
                        isServerDown = true;
                        richTextBox1.AppendText("Server: " + data + "\n");
                        setUItoNotConnected();
                        clientSocket.Close();
                        connected = false;
                        username = "";
                    }
                    else if (code == "INF")//info
                    {
                        richTextBox1.AppendText("Server: " + data + "\n");
                    }
                    else if (code == "ACPT")//user accepted
                    {
                        richTextBox1.AppendText("Server: " + data + "\n");
                        button_sendUsername.Enabled = false;
                        button_requestAllSweets.Enabled = true;
                        button_sendSweet.Enabled = true;
                        textBox_enterSweet.Enabled = true;
                        button_reqUsername.Enabled = true;
                        buttonFollow.Enabled = true;
                        textBoxFollowUser.Enabled = true;
                        buttonFollowedUsersTweet.Enabled = true;
                        button_FollowersReq.Enabled = true;
                        button_FollowedUsers.Enabled = true;
                        button_DeleteSweet.Enabled = true;
                        button_block.Enabled = true;

                    }
                    else if(code == "COAU")
                    {
                        richTextBoxFeed.Clear();
                        int number_of_data = Int32.Parse(data);
                        if (number_of_data == 0)
                        {
                            richTextBox1.AppendText("No users in server\n");
                        }
                        for (int i = 0; i < number_of_data; i++)
                        {
                            Array.Clear(buffer, 0, buffer.Length);

                            clientSocket.Receive(buffer);
                            incomingMessage = Encoding.Default.GetString(buffer);
                            incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                            sendMessage(ref clientSocket, "ACK", 64);
                            richTextBoxFeed.AppendText(incomingMessage);


                        }
                    }
                    else if (code == "RQSR")
                    {
                        richTextBoxFeed.Clear();
                        bool sweetFeedUpdated = false;
                        int number_of_data = Int32.Parse(data);
                        if (number_of_data == 0)
                        {
                            richTextBox1.AppendText("Either you are not following anyone or no sweets were sent by the users you followed.\n");
                        }
                        for (int i = 0; i < number_of_data; i++)  
                        {
                            Array.Clear(buffer, 0, buffer.Length);
                            string sweetComplate = "";
                            clientSocket.Receive(buffer);
                            incomingMessage = Encoding.Default.GetString(buffer);
                            incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                            sweetComplate += incomingMessage;
                            string[] sweetSplit = sweetComplate.Split('&');
                            string userSent = sweetSplit[2];
                            if (userSent != username)
                            {
                                sweetFeedUpdated = true;
                                string sweetFinal = "[" + sweetSplit[0] + " | " + sweetSplit[3] + "] " + sweetSplit[2] + ": " + sweetSplit[1];
                                richTextBoxFeed.AppendText(sweetFinal + "\n");
                            }                         
                            sendMessage(ref clientSocket, "ACK", 64);
                        }
                        if (!sweetFeedUpdated && number_of_data != 0)
                        {
                            richTextBoxFeed.AppendText("Server only have your sweets.\n");
                        }
                    }
                    else if(code == "RQDSD")//bu comment outlananlari server da yollayabilir burada yazmaya gerek yok ama data buysa serverdan almak daha mantikli
                    {//request delete sweet - succesfully deleted
                        //richTextBoxFeed.AppendText("The sweet is succesfully deleted!\n");
                        richTextBox1.AppendText("Server: " + data + "\n");
                    }
                    else if(code == "RQDSNF") //request delete sweet - not found
                    {
                        //richTextBoxFeed.AppendText("There is no such sweet exists.\n");
                        richTextBox1.AppendText("Server: " + data + "\n");
                    }
                    else if(code == "RQBUS")//this part shold be checked, might not work due to splitting part of the server
                    {
                        richTextBox1.AppendText("Server: " + data + "\n");//data --> a message saying user is blocked and if followed, a message dsaying it is rmoved from the necessary list.
                    }//if followed before, server should remove it from the followed list and the follower list
                    else if(code == "RQBUF")
                    {
                        //richTextBoxFeed.AppendText("Either there is not such user to block or sevrer has failed to block " + userToBlock + ".\n");
                        richTextBox1.AppendText("Server: " + data + "\n");
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        if(serverCrash)
                        {
                            richTextBox1.AppendText("The server has disconnected\n");
                        }
                        setUItoNotConnected();
                    }

                    clientSocket.Close();
                    connected = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                serverCrash = true;
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                string IP = textBox_IP.Text;

                int portNum;
                if (Int32.TryParse(textBox_port.Text, out portNum))
                {
                    try
                    {
                        clientSocket.Connect(IP, portNum);
                        button_connect.Enabled = false;
                        button_disconnect.Enabled = true;
                        button_sendUsername.Enabled = true;
                        textBox_username.Enabled = true;
                        textBox_port.Enabled = false;
                        textBox_IP.Enabled = false;
                        connected = true;
                        richTextBox1.AppendText("Please provide username to complete connection.\n");

                    }
                    catch
                    {
                        richTextBox1.AppendText("Could not connect to the server!\n");
                    }
                }
                else
                {
                    richTextBox1.AppendText("Check the port.\n");
                }


                Thread receiveThread = new Thread(Receive);
                receiveThread.Start();
                isThreadCalled = true;
            }
            catch (Exception)
            {
                richTextBox1.AppendText("Could not connect to the server\n");
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if ((!isServerDown) && (isThreadCalled))
                {
                    string requestMessage;
                    if (username == "")
                    {
                        requestMessage = "DC:undefined";
                    }
                    else
                    {
                        requestMessage = "DC:" + username;
                    }
                    if (username != "")
                    {
                        sendMessage(ref clientSocket, requestMessage, 64);
                    }

                }

                connected = false;
                terminating = true;
            }
            catch (Exception)
            {

            }
        }

        private void button_sendSweet_Click(object sender, EventArgs e)
        {
            string sweet = textBox_enterSweet.Text;
            textBox_enterSweet.Clear();
            if (sweet.Contains(":") || sweet.Contains("&"))
            {
                richTextBox1.AppendText("Sweet cannot contain ':' and '&' !\n");
                return;
            }
            if (sweet != "" && sweet.Length <= 900)
            {
                string sweetMessage = "SW:" + sweet + "&" + username;
                sendMessage(ref clientSocket, sweetMessage);
                richTextBox1.AppendText("Sweet sent!\n");
            }
            else
            {
                richTextBox1.AppendText("Sweet not sent!\n");
            }
        }

        private void sendMessage(ref Socket socket, string message, int size = 1024)
        {
            if (message != "" && message.Length <= size)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                socket.Send(buffer);
            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {

            string requestMessage;
            if (username == "")
            {
                requestMessage = "DC:undefined";
            }
            else
            {
                requestMessage = "DC:" + username;
            }
            sendMessage(ref clientSocket, requestMessage, 64);


            serverCrash = false;
            connected = false;
            button_connect.Enabled = true;
            button_disconnect.Enabled = false;
            button_sendUsername.Enabled = false;
            button_sendSweet.Enabled = false;
            button_requestAllSweets.Enabled = false;
            textBox_enterSweet.Enabled = false;
            textBox_username.Enabled = false;
            textBox_IP.Enabled = true;
            textBox_port.Enabled = true;
            button_reqUsername.Enabled = true;
            buttonFollow.Enabled = false;
            textBoxFollowUser.Enabled = false;

            richTextBox1.AppendText("You are disconnected form the server.\n");
            username = "";

            clientSocket.Close();
        }

        private void button_requestAllSweets_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(username + ", you have requested all sweets and if present, they will be displayed below.\n");
            string requestMessage = "RQS:" + username;
            sendMessage(ref clientSocket, requestMessage, 64);
        }

        private void button_reqUsername_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(username + ", you have requested the username list and it will be displayed below.\n");
            string requestMessage = "RAU:";
            sendMessage(ref clientSocket, requestMessage, 64);
        }

        private void buttonFollow_Click(object sender, EventArgs e)
        {
            if(textBox_username.Text != textBoxFollowUser.Text)
            {
                if (textBoxFollowUser.Text != "")
                {
                    richTextBox1.AppendText(username + ", you have requested following " + textBoxFollowUser.Text + "\n");
                    string requestMessage = "FUR:" + textBox_username.Text + "," + textBoxFollowUser.Text;
                    sendMessage(ref clientSocket, requestMessage, 64);
                }
                else
                {
                    richTextBox1.AppendText("Follow field cannot be empty.\n");
                }
            }
            else
            {
                richTextBox1.AppendText("You can not follow yourself.\n");
            }
            
        }

        private void buttonFollowedUsersTweet_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(username + ", you have requested sweets from your followed users and if available they will be displayed below.\n");
            string requestMessage = "RQSF:" + textBox_username.Text;
            sendMessage(ref clientSocket, requestMessage, 64);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //RQLOF request list of followers
            richTextBox1.AppendText(username + " you have requested the list of your followers and if avaliable it will be displayed below.\n");
            string reqMessage = "RQLOF:" + textBox_username.Text;
            sendMessage(ref clientSocket, reqMessage, 64);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //RQLOFD request list of followed users
            richTextBox1.AppendText(username + " you have requested the list of users that you follow and if available it will be displayed below.\n");
            string reqMessage = "RQLOFD:" + textBox_username.Text;
            sendMessage(ref clientSocket, reqMessage, 64);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //delete sweet

            string sweet = textBox_enterSweet.Text;

            if (sweet == "")
            {

                richTextBox1.AppendText("Please provide a sweet ID to delete a sweet.\n");

            }
            else
            {
                int i;
                if (!int.TryParse(textBox_enterSweet.Text, out i))
                {
                    richTextBox1.Text = "This is a number only field";
                    return;
                }
                else
                {
                    string sweetID = textBox_enterSweet.Text;
                    string sweetMessage = "DSW:" + sweetID + "&" + username;//delete sweet
                    sendMessage(ref clientSocket, sweetMessage);
                    richTextBox1.AppendText("Your request for deleting this sweet is sent!\n");

                }
            }


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBoxFeed_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_block_Click(object sender, EventArgs e)
        {

            if (textBox_username.Text != textBoxFollowUser.Text)
            {
                if(textBoxFollowUser.Text != "")
                {
                    userToBlock = textBoxFollowUser.Text;
                    richTextBox1.AppendText(username + " you have requested to block " + userToBlock + " .\n");
                    string requestMessage = "RQBU:" + username + "&" + userToBlock;//bu server sideinda checklenmeli
                    sendMessage(ref clientSocket, requestMessage, 64);
                }
                else
                {
                    richTextBox1.AppendText("Block field cannot be empty.\n");
                }
                
            }
            else
            {
                richTextBox1.AppendText("You can not block yourself.\n");
            }
        }
    }
}
