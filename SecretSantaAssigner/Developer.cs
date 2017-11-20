using System.Net.Mail;

namespace SecretSantaAssigner
{

    public class Developer
    {
        private string m_strFirstName;

        private string m_strLastName;

        private Developer m_GiftRecipient;

        public MailAddress EmailAddress
        {
            get
            {
                MailAddress mailAddress = new MailAddress(string.Format("{0}.{1}@{2}", this.FirstName, this.LastName, SystemVariables.Domain));
                return mailAddress;
            }
        }

        public string FirstName
        {
            get
            {
                return this.m_strFirstName;
            }
        }

        public string FullName
        {
            get
            {
                return string.Concat(this.FirstName, " ", this.LastName);
            }
        }

        public Developer GiftRecipient
        {
            get
            {
                return this.m_GiftRecipient;
            }
            set
            {
                this.m_GiftRecipient = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.m_strLastName;
            }
        }

        public Developer(string strFirstName, string strLastName)
        {
            this.m_strFirstName = strFirstName;
            this.m_strLastName = strLastName;
        }
    }
}
