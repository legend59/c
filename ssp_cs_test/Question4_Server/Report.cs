using System;

namespace Question4_Server
{
    public class Report
    {
        private String strID;
        private int CheckCard;
        private int FailCard;

        public String getStrID()
        {
            return strID;
        }
        public void setStrID(String strID)
        {
            this.strID = strID;
        }
        public int getCheckCard()
        {
            return CheckCard;
        }
        public void setCheckCard(int nCheckCard)
        {
            this.CheckCard = nCheckCard;
        }
        public int getFailCard()
        {
            return FailCard;
        }
        public void setFailCard(int nFailCard)
        {
            this.FailCard = nFailCard;
        }
        public void increaseCheckCard()
        {
            this.CheckCard++;
        }
        public void increaseFailCard()
        {
            this.FailCard++;
        }
    }
}