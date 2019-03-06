namespace Campaign
{
    public class CampaignInitializer
    {
        public static CampaignNode InitializeCampaign()
        {
            //Initialize Tutorial Mission Instantiators
            WinCondition winCon = new WinCondition();
            TutorialMoneyInitialier moneyInit = new TutorialMoneyInitialier();

            //Initialize Tutorial Missin Triggers
            WinTrigger win = new WinTrigger();
            SpeakTrigger tutorial_First_Mayor = new SpeakTrigger(SpeakerType.Mayor, 1, "I will get your medicine");
            SpeakTrigger tutorial_First_Shopkeeper = new SpeakTrigger(SpeakerType.ShopKeeper, 1, "Do you know where to find money?");
            PickupTrigger tutorialMoneyCollection = new PickupTrigger("Money", 5);
            SpeakTrigger tutorial_Second_Shopkeeper = new SpeakTrigger(SpeakerType.ShopKeeper, 1, "Here is the money you asked for");
            PickupTrigger pickUpMedicine = new PickupTrigger("Mayor_Medicine", 1);
            SpeakTrigger tutorial_Second_Mayor = new SpeakTrigger(SpeakerType.Mayor, 1, "Here is your medicine");

            CampaignNode temp;
            CampaignNode next;

            //Contruct the campaign tree 
            next = new CampaignNode(null, null, win, 7, "You Win!", winCon);
            temp = new CampaignNode(next, null, tutorial_Second_Mayor, 6, "Return to the Mayor to give him his medicine");
            //next = temp;
            //temp = new CampaignNode(next, null, pickUpMedicine, 5, "Pickup Medicine from the Shopkeeper");
            next = temp;
            temp = new CampaignNode(next, null, tutorial_Second_Shopkeeper, 4, "Return to the Shopkeeper with the money");
            next = temp;
            temp = new CampaignNode(next, null, tutorialMoneyCollection, 3, "Collect 5 money to pay the Shopkeeper for the Mayor's medicine", moneyInit);
            next = temp;
            temp = new CampaignNode(next, null, tutorial_First_Shopkeeper, 2, "Talk to the Shopkeeper about the Mayor's medicine");
            next = temp;
            temp = new CampaignNode(next, null, tutorial_First_Mayor, 1, "Talk to the Mayor");
            return temp;
        }
    }
}