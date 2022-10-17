namespace SE_Semester_Project
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            

            //to be deleted \/
            Message message = new Message();
            message.smilFileName = "Generated-Smil.smil";
            message.receiverName = "Mitchel";
            message.senderName = "Coleman";
            TextMessage textMessage = new TextMessage();
            textMessage.text = "Please Work";
            textMessage.duration = "5m,15s";
            textMessage.beginTime = "3s";
            message.AddTextMessage(textMessage);
            TextMessage textMessage2 = new TextMessage();
            textMessage2.text = "Please Work";
            textMessage2.duration = "5m,15s";
            textMessage2.beginTime = "3s";
            message.AddTextMessage(textMessage2);
            message.generateMessageFile();
            //to be deleted /\
        }
    }
}