using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;
using System.Net;

namespace SecretSantaAssigner
{

    class Program
    {
        static void Main(string[] args)
        {
            string ParticpantsFile = "";
            string EmailBodyFile = "";
            bool PrintAssignments = false;
            bool SendEmails = false;

            if (ParseCommandLineArgs(args, ref ParticpantsFile, ref EmailBodyFile, ref PrintAssignments, ref SendEmails))
            {
                Assigner assigner = new Assigner(ParticpantsFile, EmailBodyFile);
                assigner.Assign(PrintAssignments, SendEmails);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        static bool ParseCommandLineArgs(string[] args, ref string ParticpantsFile, ref string EmailBodyFile, ref bool PrintAssignments, ref bool SendEmails)
        {
            for (var i = 0; i < args.Length; ++i)
            {
                string strCommand = args[i];

                if (string.Compare(strCommand, "-participants", true) == 0)
                {
                    if (i < args.Length - 1)
                    {
                        ParticpantsFile = args[i + 1];
                        i++;
                    }
                }
                else if (string.Compare(strCommand, "-email", true) == 0)
                {
                    if (i < args.Length - 1)
                    {
                        EmailBodyFile = args[i + 1];
                        i++;
                    }
                }
                else if (string.Compare(strCommand, "-print", true) == 0)
                {
                    PrintAssignments = true;
                }
                else if (string.Compare(strCommand, "-send", true) == 0)
                {
                    SendEmails = true;
                }
                else if (string.Compare(strCommand, "-help", true) == 0)
                {
                    string helpMessage = "\r\n\r\n**********************************************************";
                    helpMessage += "\r\nHO! HO! HO! Welcome to the Secret Santa Assigner help menu";
                    helpMessage += "\r\n**********************************************************";
                    helpMessage += "\r\n\r\nPlease note that this Secret Santa Assigner was built to send emails using notifications@miacanalytics.com as the sending account. Additionally, it assumes that each participant has a valid miacanalytics email account constructed by concatenating their first name, a period, and their last name.";
                    helpMessage += "\r\n\r\nPlease use the following arguments to assign and send secret santas:";
                    helpMessage += "\r\n\r\n-participants <file path>: the path to the .txt file that holds the names of the participants in your Secret Santa Event. Ensure that each line in this file is its own participant; ensure that each participant includes a valid first and last name separated by one space.";
                    helpMessage += "\r\n\r\n-email <file path>: the path to the .txt file that includes the body of the email to be sent. This application will replace {SECRET_SANTA} with the secret santa participant and {RECIPIENT} with their recipient.";
                    helpMessage += "\r\n\r\n-print: include this if you would like to see the printed assignments.";
                    helpMessage += "\r\n\r\n-send: include this if you would like to send the assignments to the included participants.";
                    helpMessage += "\r\n\r\n-help: display this menu.";
                    Console.WriteLine(helpMessage);
                    return false;
                }
            }
            return true;
        }
    }
}
