using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Switter
{

    public struct Sweet
    {
        public string username;
        public int id;
        public string sweetData;
        public string sweetTime;
    }


    public partial class Server : Form
    {
        List<string> database;
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<String> connectedClientUsernames = new List<String>();
        List<Sweet> sweetDatabse = new List<Sweet>();
        List<String> followerDatabase = new List<String>();

        int currentID = 0;

        bool terminating = false;
        bool listening = false;

        string getPathOfFile(string filename)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = Path.Combine(projectDirectory, @"user-db.txt");
            return path;
        }

        public Server()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = getPathOfFile("user-db.txt");
            if (!File.Exists(path))
            {
                MessageBox.Show("Users.txt file does not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            database = File.ReadLines(path).ToList();
            
            labelIpAddress.Text = "Your Ip Address: " + Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        }
        private bool checkUserNameExistsInDatabase(string username)
        {
            return database.Contains(username);
        }

        private bool checkUserNameExistsInLoggedUsers(string username)
        {
            return !connectedClientUsernames.Contains(username);
        }

        private void Server_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            serverSocket.Close();
            Environment.Exit(0);
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBoxPort.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(999);

                listening = true;
                buttonListen.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.IsBackground = true;
                acceptThread.Start();
                richTextBoxLogs.AppendText("Started listening on port: " + serverPort + "\n");
            }
            else
            {
                richTextBoxLogs.AppendText("Please check port number \n");
            }
        }

        private string getInComingMessage(Socket socket, int size = 1024)
        {
            Byte[] buffer = new Byte[size];
            socket.Receive(buffer);
            string incomingMessage = Encoding.Default.GetString(buffer);
            incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
            return incomingMessage;
        }

        private void sendMessage(ref Socket socket, string message, int size = 1024)
        {
            if (message != "" && message.Length <= size)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                socket.Send(buffer);
            }
        }

        private List<string> getSweetsOfUsers(List<string> users)
        {
            List<string> newDatas = new List<string>();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "sweets.txt");
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splitedLine = lines[i].Split('&');
                    if (users.Contains(splitedLine[2]))
                    {
                        newDatas.Add(lines[i]);
                    }
                }
            }
            return newDatas;
        }

        private List<string> findBlockedUsers(string username, string filename = "block_database.txt")
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            List<string> blockersList = new List<string>();
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splitted = lines[i].Split(':');
                    splitted[1] += ",";
                    string[] blockers = splitted[1].Split(',');
                    if (blockers.Contains(username))
                    {
                        blockersList.Add(splitted[0]);
                    }
                }
            }
            return blockersList;
        }

        private List<string> getSweetsFromFile(string username, string filename = "sweets.txt")
        {
            List<string> newDatas = new List<string>();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            string path_block = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "block_database.txt");

            if (File.Exists(path))
            {
                List<string> blockedUsers = findBlockedUsers(username);
                List<string> lines = File.ReadLines(path).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    string splitted = lines[i].Split('&')[2];
                    if (!blockedUsers.Contains(splitted))
                    {
                        newDatas.Add(lines[i]);
                    }
                }
            }
            return newDatas;
        }

        void removeSweet(ref Socket socket, string username, string sweetId, string filename = "sweets.txt")
        {
            bool isAlredyRejected = false;
            List<string> newDatas = new List<string>();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            if (File.Exists(path))
            {
                bool isExists = false;
                List<string> lines = File.ReadLines(path).ToList();
                List<string> withOutDeletedOne = new List<string>();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splittedData = lines[i].Split('&');
                    if (splittedData[0] == sweetId && splittedData[2] == username)
                    {
                        isExists = true;
                    } 
                    else if(splittedData[0] == sweetId && splittedData[2] != username)
                    {
                        isAlredyRejected = true;
                        sendMessage(ref socket, "INF:" + "This sweet does not belong to you. So you can not delete it!");
                        richTextBoxLogs.AppendText("Delete request for " + sweetId + " is rejected it does not belong to requst sender.\n");
                    }
                    else
                    {
                        withOutDeletedOne.Add(lines[i]);
                    }
                    newDatas.Add(lines[i]);
                }

                if (isExists == true)
                {
                    appendAllToTxt(withOutDeletedOne, "sweets.txt");
                    sendMessage(ref socket, "INF:" + "The sweet is removed from the database!");
                    richTextBoxLogs.AppendText("The sweet with "+ sweetId +" is deleted.");
                }
                else
                {
                    if(!isAlredyRejected)
                    {
                        richTextBoxLogs.AppendText("Delete request for " + sweetId + " is rejected it is not in database.\n");
                        sendMessage(ref socket, "INF:" + "There is not a sweet with this ID!");
                    }
                    
                }
            }
        }

        private void removeFromFollowers(string A, string B)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "follow_database.txt");
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splitted = lines[i].Split(':');
                    if (splitted[0] == B)
                    {
                        lines[i] = splitted[0] + ':' + splitted[1].Replace(A + ',', "");
                    }
                }
                appendAllToTxt(lines, "follow_database.txt");
            }
        }

        private void  blockUser(string username, string blockedUser) {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "block_database.txt");
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                bool isFromUserExists = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splittedLine = lines[i].Split(':');
                    if (splittedLine[0] == username)
                    {
                        isFromUserExists = true;
                        lines[i] = lines[i] + "," + blockedUser;
                        removeFromFollowers(username, blockedUser);
                        break;
                    }
                }

                if (isFromUserExists == false)
                {
                    lines.Add(username + ":" + blockedUser);
                    removeFromFollowers(username, blockedUser);
                }
                File.WriteAllLines("block_database.txt", lines);
            }
            else
            {
                appendToTxt(username + ":" + blockedUser, "block_database.txt");
            }
        }

        private void appendAllToTxt(List<string> newLines, string filename)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            File.WriteAllLines(path, newLines);
        }

        private void appendToTxt(string data, string filename = "sweets.txt")
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(data);
            }
        }

        private void updateID(ref int id, string filename = "sweets.txt")
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                if (lines.Count == 0)
                {
                    id = 0;
                }
                else
                {
                    string[] idS = lines[lines.Count - 1].Split('&');
                    int idInt = Int32.Parse(idS[0]);
                    id = idInt;
                }
            }
            else
            {
                id = 0;
            }
        }

        private List<string> getListOfUsernames()
        {
            List<string> list_of_messages_512 = new List<string>();
            string current_message = "";

            for(int i = 0; i < database.Count; i++)
            {
                if (current_message.Length > 512 || i == database.Count - 1)
                {
                    list_of_messages_512.Add(current_message);
                    current_message = "";
                }
                else
                {
                    current_message += database[i] + "\n"; 
                }
            }

            return list_of_messages_512;
        }

        private void addFollowData(string[] followData)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "follow_database.txt");
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                bool isFromUserExists = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splittedLine = lines[i].Split(':');
                    if (splittedLine[0] == followData[0])
                    {
                        isFromUserExists = true;
                        lines[i] = lines[i] + followData[1] + ',';
                        break;
                    }
                }

                if (isFromUserExists == false)
                {
                    lines.Add(followData[0] + ":" + followData[1] + ',');
                }
                File.WriteAllLines("follow_database.txt", lines);
            } 
            else
            {
                appendToTxt(followData[0] + ":" + followData[1] + ',', "follow_database.txt");
            }
        }
        private bool checkUserInBlockList(string username, string username2)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "block_database.txt");
            bool blockedFound = false;
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splittedLine = lines[i].Split(':');
                    if (splittedLine[0] == username && splittedLine[1].Contains(username2))
                    {
                        blockedFound = true;
                        break;
                    }
                }
            }
            return blockedFound;
        }

        private bool checkUserFollowsTheUser(string username, string username2)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "follow_database.txt");
            bool followFound = false;
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splittedLine = lines[i].Split(':');
                    if (splittedLine[0] == username && splittedLine[1].Contains(username2))
                    {
                        followFound = true;
                        break;
                    }
                }
            }
            return followFound;
        }

        private bool checkUserIsBlocked(string[] from_to)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "block_database.txt");
            if (File.Exists(path))
            {
                List<string> lines = File.ReadLines(path).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] splittedLine = lines[i].Split(':');
                    if (splittedLine[0] == from_to[1])
                    {
                        string[] splitData = splittedLine[1].Split(',');
                        return splitData.Contains(from_to[0]);
                    }
                }
            }
            return false;
        }

        private List<string> getFollowers(string username)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "follow_database.txt");
            List<string> followers = new List<string>();
            if (File.Exists(path))
            {
                List<string> follow_database = File.ReadLines(path).ToList();
                for (int i = 0; i < follow_database.Count; i++)
                {
                    string[] splitted = follow_database[i].Split(':');
                    List<string> followData = splitted[1].Split(',').ToList();
                    if (followData.Contains(username))
                    {
                        followers.Add(splitted[0]);
                    }
                }
            }
            return followers;
        }

        private List<string> getFollowedUsers(string username)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "follow_database.txt");
            List<string> followed = new List<string>();
            if (File.Exists(path))
            {
                List<string> follow_database = File.ReadLines(path).ToList();
                for (int i = 0; i < follow_database.Count; i++)
                {
                    string[] splitted = follow_database[i].Split(':');
                    if (splitted[0] == username)
                    {
                        return splitted[1].Split(',').ToList();
                    }
                }
            }
            return followed;
        }

        private void doVoid(ref Socket socket, ref bool connected, ref string incomingMessage)
        {
            lock (this)
            {
                string messageDisconnect = "";
                string[] splittedMessage = incomingMessage.Split(':');
                string code = splittedMessage[0];
                string data = splittedMessage[1];


                if (code == "UD")
                {
                    if (checkUserNameExistsInDatabase(data) && checkUserNameExistsInLoggedUsers(data))
                    {
                        connectedClientUsernames.Add(data);

                        richTextBoxLogs.AppendText("User: " + data + " is loged.\n");
                        sendMessage(ref socket, "ACPT:Your connection is complated " + data + ":)", 64);
                    }
                    else
                    {
                        if (!terminating)
                        {
                            if (checkUserNameExistsInLoggedUsers(data) == false)
                            {
                                richTextBoxLogs.AppendText("This user is already connected\n");
                                messageDisconnect = "DC:This user is already connected, your connection failed";
                            }
                            else
                            {
                                richTextBoxLogs.AppendText("There is no user with this username.\n");
                                messageDisconnect = "DC:There is no user with this username, your connection failed";
                                ;
                            }
                        }
                        if (messageDisconnect != "")
                        {
                            sendMessage(ref socket, messageDisconnect, 64);
                        }
                        socket.Close();
                        clientSockets.Remove(socket);
                        connected = false;
                        richTextBoxLogs.AppendText("Rejected a connection\n");
                    }
                }
                else if (code == "DC")
                {
                    socket.Close();
                    clientSockets.Remove(socket);
                    if (data != "undefined")
                    {
                        connectedClientUsernames.Remove(data);
                    }
                    connected = false;
                    richTextBoxLogs.AppendText("Connection with a client ended\n");
                }
                else if (code == "SW") //sweet recive
                {
                    updateID(ref currentID);
                    currentID++;
                    string newData = currentID.ToString() + "&" + splittedMessage[1] + "&" + DateTime.Now.ToString("dd/MM/yyyy h:mm");
                    
                    appendToTxt(newData);
                    richTextBoxLogs.AppendText("New sweet from " + splittedMessage[1].Split('&')[1] + "\n");
                    
                }
                else if (code == "DSW") //sweet recive
                {
                    string[] sweetIdAndUsername = splittedMessage[1].Split('&');
                    // id ve username 
                    removeSweet(ref socket, sweetIdAndUsername[1], sweetIdAndUsername[0]);
                }
                else if (code == "RQBU")
                {
                    string[] from_to = data.Split('&');

                    if (!checkUserNameExistsInDatabase(from_to[1]))
                    {
                        richTextBoxLogs.AppendText("Block request to " + from_to[1] + " rejected because user is not in database.\n");
                        sendMessage(ref socket, "INF:" + "This user is not in database!");
                    }
                    else
                    {
                        if (checkUserInBlockList(from_to[0], from_to[1]))
                        {
                            sendMessage(ref socket, "INF:" + "You have already blocked this user!");
                            richTextBoxLogs.AppendText(from_to[0] + " have already blocked this user " + from_to[1] + "\n");
                        }
                        else
                        {
                            blockUser(from_to[0], from_to[1]);
                            sendMessage(ref socket, "INF:" + "You blocked " + from_to[1] + "!");
                            richTextBoxLogs.AppendText(from_to[0] + " blocked " + from_to[1] + "\n");
                        }
                    }
                }
                else if (code == "FUR")
                {
                    string[] from_to = data.Split(',');

                    if (!checkUserNameExistsInDatabase(from_to[1]))
                    {
                        sendMessage(ref socket, "INF:" + "This user is not in database!");
                        richTextBoxLogs.AppendText("Follow requst rejected because user is not in database \n");
                    }
                    else
                    {
                        if (checkUserFollowsTheUser(from_to[0], from_to[1]))
                        {
                            sendMessage(ref socket, "INF:" + "You are already following this user!");
                            richTextBoxLogs.AppendText(from_to[0] + " is already following " + from_to[1] + "\n");
                        }
                        else if(checkUserIsBlocked(from_to))
                        {
                            sendMessage(ref socket, "INF:" + "You can not follow this user!");
                            richTextBoxLogs.AppendText(from_to[0] + " cannot follow this user because of " + from_to[1] + " blocked by " + from_to[0] + "\n");
                        }
                        else
                        {
                            addFollowData(from_to);
                            sendMessage(ref socket, "INF:" + "You followed " + from_to[1] + "!");
                            richTextBoxLogs.AppendText(from_to[0] + " followed " + from_to[1] + "\n");
                        }
                    }

                }
                else if (code == "RQS") //sent all sweets
                {
                    List<string> newDatas = getSweetsFromFile(data);

                    sendMessage(ref socket, "RQSR:" + newDatas.Count().ToString(), 64);

                    int counter = 0;
                    while (counter != newDatas.Count)
                    {
                        sendMessage(ref socket, newDatas[counter]);
                        string returnMessage = getInComingMessage(socket);

                        if (returnMessage == "ACK")
                        {
                            counter++;
                        }
                    }
                } 
                else if (code == "RAU") // sent all usernames
                {
                    // usernameleri 512 charli stringin icersinde tutmamiz lazim
                    // 5 tane 512 olsa o zaman 5 tane request aticaz
                    // diger tarafta (client) 5 gorucek  5 tane alicak daha sonra

                    List<string> newDatas = getListOfUsernames();

                    sendMessage(ref socket, "COAU:" + newDatas.Count().ToString(), 64);

                    int counter = 0;
                    while (counter != newDatas.Count)
                    {
                        sendMessage(ref socket, newDatas[counter]);
                        string returnMessage = getInComingMessage(socket);

                        if (returnMessage == "ACK")
                        {
                            counter++;
                        }
                    }
                }
                else if (code == "RQSF") // sent followed users tweets
                {
                    List<string> followed_users = getFollowedUsers(data);
                    List<string> newDatas = getSweetsOfUsers(followed_users);
                    sendMessage(ref socket, "RQSR:" + newDatas.Count().ToString(), 64);

                    int counter = 0;
                    while (counter != newDatas.Count)
                    {
                        sendMessage(ref socket, newDatas[counter]);
                        string returnMessage = getInComingMessage(socket);

                        if (returnMessage == "ACK")
                        {
                            counter++;
                        }
                    }
                }
                else if (code == "RQLOF") // sent followed users tweets
                {
                    List<string> followers = getFollowers(data);
                    sendMessage(ref socket, "COAU:" + followers.Count().ToString(), 64);

                    int counter = 0;
                    while (counter != followers.Count)
                    {
                        sendMessage(ref socket, followers[counter] + '\n');
                        string returnMessage = getInComingMessage(socket);

                        if (returnMessage == "ACK")
                        {
                            counter++;
                        }
                    }
                }
                else if (code == "RQLOFD")
                {
                    List<string> followedUsers = getFollowedUsers(data);

                    if (followedUsers.Count == 0)
                    {
                        sendMessage(ref socket, "INF:" + "You are not following anyone!");
                    }
                    else
                    {
                        sendMessage(ref socket, "COAU:" + followedUsers.Count().ToString(), 64);

                        int counter = 0;
                        while (counter != followedUsers.Count)
                        {
                            sendMessage(ref socket, followedUsers[counter] + '\n');
                            string returnMessage = getInComingMessage(socket);

                            if (returnMessage == "ACK")
                            {
                                counter++;
                            }
                        }
                    }

                }
            }
        }

        private bool checkIncomingMessage(string incomingMessage)
        {
            if (incomingMessage.Contains(':'))
            {
                return true;
            }
            return false;
        }

        private void Receive(Socket socket)
        {
            bool connected = true;

            while (connected && !terminating)
            {
                string username = "";
                try
                {
                    string incomingMessage = getInComingMessage(socket);
                    if (checkIncomingMessage(incomingMessage))
                    {
                        doVoid(ref socket, ref connected, ref incomingMessage);
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        richTextBoxLogs.AppendText("A client has disconnected\n");
                    }
                    socket.Close();
                    clientSockets.Remove(socket);
                    if (username != "")
                    {
                        connectedClientUsernames.Remove(username);
                    }
                    connected = false;
                }
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    richTextBoxLogs.AppendText("A client is trying to connect.\n");
               
                    Thread receiveThread = new Thread(() => Receive(newClient)); // updated
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richTextBoxLogs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

    }
}
