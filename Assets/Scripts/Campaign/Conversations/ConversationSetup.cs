using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Campaign
{
    class ConversationInitializer
    {
        public static LinkedListCell<SpeakerType, LinkedListCell<int, LinkedListCell<int, ConversationTree>>> InitializeConversations()
        {
            //Create ConversationTrees for different points in the dialog
            ConversationTree tutorialMayor1 = new ConversationTree(
                "Thank goodness you have arrived! I have run out of my VirusBeGone medication! Would you be willing to help me?",
                new string[2] { "Definitely, what do I need to do?", "No thanks, I am busy right now. Maybe later." },
                new ConversationTree[2] {
                    new ConversationTree(
                        "Thank you so much! If you could run to the store to pick up some for me it would be greatly appreciated Just tell the shopkeeper that I sent you and he should know what I need",
                        new string[1]{ "No problem, be back soon!" },
                        new ConversationTree[1]{
                            new ConversationTree(
                             "/SE_I will get your medicine",
                             new string[1] { "" },
                             new ConversationTree[1] { null }
                         )
                        }
                    )
                 , new ConversationTree(
                     "I'm sorry to hear that, please let me know when you change your mind",
                     new string[1]{ "Will do!" },
                     new ConversationTree[1]{
                         null
                     }
                     )
                }
                                                                   );

            ConversationTree tutorialMayor2 = new ConversationTree("Thank you for picking up my medicine from the shop! These pesky viruses are impacting everyones health here in Cyberville!",
                                                                        new string[1] { "I am sorry to hear that! Is there anything that I can do to help?" },
                                                                        new ConversationTree[1] { new ConversationTree("In fact there is! Throughout this game you will learn about the different threats that we here in Cyberville face on a regular basis and what steps you can take to protect yourslef and others around you! Will you be willing to take on the challenge and become our next defender from the dreaded DarkNet?",
                                                                                                                        new string[1]{ "Definitely! When do I start?" },
                                                                                                                        new ConversationTree[1]{ new ConversationTree("Right Now! There is a lot to learn before you can try to take on the evil that lies in the DarkNet!",
                                                                                                                                                                       new string[1]{ "I can't wait!" },
                                                                                                                                                                       new ConversationTree[1]{ new ConversationTree("/SE_Here is your medicine", new string[1] { "" }, new ConversationTree[1] { null })}
                                                                                                                                                                      )
                                                                                                                                                }
                                                                                                                        )
                                                                                                }
                                                                        );
            ConversationTree tutorialShopkeeper1_3 = new ConversationTree("Oh thats easy, you can normally find money just lying around town! People aren't overly careful here in Cyberville", 
                                                                          new string[1] { "That seems a bit insecure, but I guess it helps out in times like these! How much money do I need?" }, 
                                                                          new ConversationTree[1] { new ConversationTree("Oh, 5 should do just fine! Let me know when you round up the money!", new string[1] { "Will do!" }, new ConversationTree[1] {new ConversationTree("/SE_Do you know where to find money?", new string[1] { "" }, new ConversationTree[1] { null }) }) }
                                                                         );

            ConversationTree tutorialShopkeeper1_2 = new ConversationTree("Oh, I was wondering when he was planning to pick that up. Did he happen to send any money with you?",
                                                                         new string[3] { "Ummmm...... no?", "I thought he paid in advance!", "No! Does he expect me to pay for his Meds?" },
                                                                         new ConversationTree[3] { new ConversationTree("Did you expect this medicine to be free?!? This isn't cheap you know!", new string[1] { "I guess that makes sense, but I don't have any money! How am I supposed to pay for it?" }, new ConversationTree[1] { tutorialShopkeeper1_3}),
                                                                                                   new ConversationTree("Ha! I wish he did!", new string[1]{ "That is annoying! I don't have any money, how I am supposed to pay?"}, new ConversationTree[1]{ tutorialShopkeeper1_3 }),
                                                                                                   new ConversationTree("I doubt it. He is just very busy being mayor. He probably just forgot about it.", new string[1]{ "Even then! How am I supposed to pay fo it?!?"}, new ConversationTree[1]{ tutorialShopkeeper1_3 })
                                                                                                    }
                                                                         );
            ConversationTree tutorialShopkeeper1 = new ConversationTree("What can I do to help?", new string[3] { "You up to trade?", "I am here to pick up the Mayor's medicine.","Thanks, but I need to run" }, new ConversationTree[3] { new ConversationTree("/OG_ShopGUI", new string[1] { "" }, new ConversationTree[1] { null }), tutorialShopkeeper1_2, null });

            ConversationTree tutorialShopkeeper2_2 = new ConversationTree("Good Job! Here is the Mayor's medicine. Hope to see you around!", new string[1] { "Thanks!" }, new ConversationTree[1] { new ConversationTree("/SE_Here is the money you asked for", new string[1] { "" }, new ConversationTree[1] { null })});

            ConversationTree tutorialShopkeeper2 = new ConversationTree("What can I do to help?", new string[3] { "You up to trade?", "I have the money you asked for.", "Thanks, but I need to run" }, new ConversationTree[3] { new ConversationTree("/OG_ShopGUI", new string[1] { "" }, new ConversationTree[1] { null }), tutorialShopkeeper2_2, null });

            //Initialize dialogue for mayor
            LinkedListCell<int, ConversationTree> Mayor1 = new LinkedListCell<int, ConversationTree>(1, tutorialMayor1);
            LinkedListCell<int, ConversationTree> Mayor2 = new LinkedListCell<int, ConversationTree>(6, tutorialMayor2);
            Mayor1.Next = Mayor2;

            //Initialize dialogue 
            LinkedListCell<int, ConversationTree> Shopkeeper1 = new LinkedListCell<int, ConversationTree>(2, tutorialShopkeeper1);
            LinkedListCell<int, ConversationTree> Shopkeeper2 = new LinkedListCell<int, ConversationTree>(4, tutorialShopkeeper2);
            Shopkeeper1.Next = Shopkeeper2;

            LinkedListCell<int, LinkedListCell<int, ConversationTree>> ShopkeepID = new LinkedListCell<int, LinkedListCell<int, ConversationTree>>(1, Shopkeeper1);
            LinkedListCell<int, LinkedListCell<int, ConversationTree>> MayorID = new LinkedListCell<int, LinkedListCell<int, ConversationTree>>(1, Mayor1);

            LinkedListCell<SpeakerType, LinkedListCell<int, LinkedListCell<int, ConversationTree>>> Mayor = new LinkedListCell<SpeakerType, LinkedListCell<int, LinkedListCell<int, ConversationTree>>>(SpeakerType.Mayor, MayorID);
            LinkedListCell<SpeakerType, LinkedListCell<int, LinkedListCell<int, ConversationTree>>> Shopkeep = new LinkedListCell<SpeakerType, LinkedListCell<int, LinkedListCell<int, ConversationTree>>>(SpeakerType.ShopKeeper, ShopkeepID);
            Mayor.Next = Shopkeep;

            return Mayor;
        }

        public static ConversationTree[] InitializeConversationDefaults()
        {
            ConversationTree[] defaults = new ConversationTree[Enum.GetNames(typeof(SpeakerType)).Length];

            defaults[(int)SpeakerType.Mayor] = new ConversationTree("Help! The city is in danger!", new string[1] { "Don't worry, I am here to help" }, new ConversationTree[1] { null });
            defaults[(int)SpeakerType.Pedestrian] = new ConversationTree("Thank goodness you are here to help!", new string[1] { "Sure, yeah, that is definitely why I am here" }, new ConversationTree[1] { null });
            defaults[(int)SpeakerType.ShopKeeper] = new ConversationTree("What can I do to help?", new string[2] { "You up to trade?", "Thanks, but I need to run" }, new ConversationTree[2] { new ConversationTree("/OG_ShopGUI", new string[1] { "" }, new ConversationTree[1] { null }), null });
            defaults[(int)SpeakerType.Villain] = new ConversationTree("Muahahaha, your world will soon be mine!", new string[1] { "Not if I can help it!" }, new ConversationTree[1] { null });

            return defaults;
        }
    }
}
