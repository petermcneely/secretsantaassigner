using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace SecretSantaAssigner
{

    public class Assigner
    {
        private List<Developer> m_Developers = new List<Developer>();

        private string m_ParticipantFileName;

        private string m_EmailFileName;

        private string m_EmailBody;

        public Assigner(string participantFile, string emailFile)
        {
            this.m_ParticipantFileName = participantFile;
            this.m_EmailFileName = emailFile;
        }

        public void Assign(bool bPrintAssignments, bool bSendEmails)
        {
            if (this.ReadDevelopers())
            {
                if (this.ReadEmail())
                {
                    while (!this.AssignSecretSantas())
                    {
                        this.ClearAssignments();
                    }
                    if (bPrintAssignments)
                    {
                        this.PrintAssignments();
                    }
                    if (bSendEmails)
                    {
                        this.SendEmails();
                    }
                }
            }
        }

        private bool AssignSecretSantas()
        {
            bool flag;
            List<Developer> hat = new List<Developer>();
            hat.AddRange(this.m_Developers);
            Random rand = new Random();
            foreach (Developer developer in this.m_Developers)
            {
                int nRandomNumber = rand.Next(0, hat.Count);
                Developer chosenDeveloper = hat.ElementAt<Developer>(nRandomNumber);
                while (chosenDeveloper.Equals(developer))
                {
                    if (hat.Count != 1)
                    {
                        nRandomNumber = rand.Next(0, hat.Count);
                        chosenDeveloper = hat.ElementAt<Developer>(nRandomNumber);
                    }
                    else
                    {
                        flag = false;
                        return flag;
                    }
                }
                developer.GiftRecipient = chosenDeveloper;
                if (!hat.Remove(chosenDeveloper))
                {
                    Console.WriteLine(string.Concat("Failed to remove ", chosenDeveloper.FullName));
                    flag = false;
                    return flag;
                }
            }
            flag = true;
            return flag;
        }

        private void ClearAssignments()
        {
            foreach (Developer dev in this.m_Developers)
            {
                if (dev.GiftRecipient != null)
                {
                    dev.GiftRecipient = null;
                }
            }
        }

        private string GetEmailBody(string strSecretSanta, string strRecipient)
        {
            string currentEmail = this.m_EmailBody.Replace("{SECRET_SANTA}", strSecretSanta);
            return currentEmail.Replace("{RECIPIENT}", strRecipient);
        }

        private void PrintAssignments()
        {
            foreach (Developer developer in this.m_Developers)
            {
                Console.WriteLine(string.Concat(developer.FullName, " was assigned to: ", developer.GiftRecipient.FullName));
            }
        }

        private bool ReadDevelopers()
        {
            bool flag;
            try
            {
                string[] strArrays = File.ReadAllLines(this.m_ParticipantFileName);
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string name = strArrays[i];
                    string[] fullName = name.Split(new char[] { ' ' });
                    this.m_Developers.Add(new Developer(fullName[0], fullName[1]));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Unable to parse the file of participant names: {0}", exception.Message));
                flag = false;
                return flag;
            }
            flag = true;
            return flag;
        }

        private bool ReadEmail()
        {
            bool flag;
            try
            {
                this.m_EmailBody = File.ReadAllText(this.m_EmailFileName);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Unable to parse the email body file: {0}", exception.Message));
                flag = false;
                return flag;
            }
            flag = true;
            return flag;
        }

        private void SendEmail(Developer dev)
        {
            MailMessage message = new MailMessage();
            message.To.Add(dev.EmailAddress);
            message.Subject = "Secret Santa Assignment";
            message.From = new MailAddress("notifications@miacanalytics.com");
            message.Body = this.GetEmailBody(dev.FirstName, dev.GiftRecipient.FullName);
            SmtpClient smtp = new SmtpClient("miacsys02.miacanalytics.net");
            try
            {
                smtp.Send(message);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SendEmails()
        {
            foreach (Developer developer in this.m_Developers)
            {
                try
                {
                    this.SendEmail(developer);
                }
                catch (Exception exception)
                {
                    Exception e = exception;
                    Console.WriteLine(string.Concat(new string[] { "The following error occurred while sending an email to ", developer.FullName, ":\r\n", e.Message, "\r\n." }));
                    return;
                }
            }
            Console.WriteLine("Successfully sent the emails!");
        }
    }
}
