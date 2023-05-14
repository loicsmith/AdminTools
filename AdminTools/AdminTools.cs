using Life;
using Life.BizSystem;
using Life.DB;
using Life.Network;
using Life.UI;
using Mirror;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

using Life.AreaSystem;
using Life.InventorySystem;
using Life.Network.Systems;
using Life.PermissionSystem;
using Life.VehicleSystem;
using System.Threading.Tasks;
using I2.Loc.SimpleJSON;
using System.Runtime.InteropServices;

namespace AdminTools
{
    public class AdminTools : Plugin
    {
        [SyncVar(hook = "OnFly")]
        public bool isFlying;
        [SyncVar(hook = "OnVanish")]
        public bool isVanished;
        public AdminTools(IGameAPI api)
          : base(api)
        {
        }

        public override void OnPluginInit()


        {
            SChatCommand schatCommand1 = new SChatCommand("/as", "admin service", "/as", (Action<Player, string[]>)((player, args) =>
      {
          if (player.IsAdmin)
          {

              if (!player.isAuthAdmin)
              {
                  player.SendText("<color=#e8472a>Vous n'êtes pas authentifié.</color>");
              }

              if (player.setup.isAdminService == true)
              {
                  player.SetAdminService(false);
                  player.SendText(string.Format("<color={0}>Service admin désactivé !</color>", (object)"#e8472a"));

                  /*  if (player.setup.isFlying == false) player.SendText(" ");
                    else
                    {
                        Nova.character.CmdAdminFly();
                    }
                    if (player.setup.isVanished == false) player.SendText(" ");
                    else
                    {
                        player.setup.CmdAdminVanish();
                    }
                  player.setup.CmdAdminSetHealth(player.character.Id, 100);
                  player.setup.CmdAdminSetHunger(player.character.Id, 100);
                  player.setup.CmdAdminSetThirst(player.character.Id, 100); */
                  player.Health = 100;
                  player.Hunger = 100;
                  player.Thirst = 100;
              }
              else
              {
                  player.SetAdminService(true);
                  player.SendText(string.Format("<color={0}>Service admin activé !</color>", (object)"#8ec765"));

                  /*   if (player.setup.isFlying == true)
                         player.SendText(" ");
                     else
                     {
                         Nova.character.CmdAdminFly();
                     }
                     if (player.setup.isVanished == true) player.SendText(" ");
                     else
                     {
                         player.setup.CmdAdminVanish();
                     }
                     player.setup.CmdAdminSetHealth(player.character.Id, 9999);
                     player.setup.CmdAdminSetHunger(player.character.Id, 9999);
                     player.setup.CmdAdminSetThirst(player.character.Id, 9999); */
                  player.Health = 9999;
                  player.Hunger = 9999;
                  player.Thirst = 9999;

              }
              player.serviceAdmin = !player.serviceAdmin;
          }
          else
          {

              player.SendText("<color=#e8472a>Vous n'êtes pas administrateur.</color>");

          }
      }));



            SChatCommand schatCommand3 = new SChatCommand("/admin", "Permet d'afficher le menu admin", "/admin", (Action<Player, string[]>)((player, arg) =>
            {
            if (player.IsAdmin)
            {

                UIPanel adminmenu = new UIPanel("Menu Administratif", UIPanel.PanelType.Tab);
                UIPanel globalmenu = new UIPanel("Menu Administratif - GLOBAL", UIPanel.PanelType.Tab);
                UIPanel samenu = new UIPanel("Menu Administratif - ADMIN", UIPanel.PanelType.Tab);
                UIPanel soinsmenu = new UIPanel("Menu Administratif - SOINS", UIPanel.PanelType.Tab);
                UIPanel terrainmenu = new UIPanel("Menu Administratif - TERRAIN", UIPanel.PanelType.Tab);
                UIPanel spawnterrainmenu = new UIPanel("Menu Administratif - TERRAIN SPAWN", UIPanel.PanelType.Tab);
                UIPanel vehiclesmenu = new UIPanel("Menu Administratif - VEHICULES", UIPanel.PanelType.Tab);
                UIPanel messagemenu = new UIPanel("Menu Administratif - MESSAGE", UIPanel.PanelType.Tab);


                    adminmenu.AddTabLine("Prendre/finir son service", (ui) =>
                {
                    if (player.IsAdmin)
                    {

                        if (!player.isAuthAdmin)
                        {
                            player.SendText("<color=#e8472a>Vous n'êtes pas authentifié.</color>");
                        }

                        if (player.setup.isAdminService == true)
                        {
                            player.SetAdminService(false);
                            player.SendText(string.Format("<color={0}>Service admin désactivé !</color>", (object)"#e8472a"));

                            /*  if (player.setup.isFlying == false) player.SendText(" ");
                              else
                              {
                                  Nova.character.CmdAdminFly();
                              }
                              if (player.setup.isVanished == false) player.SendText(" ");
                              else
                              {
                                  player.setup.CmdAdminVanish();
                              }
                            player.setup.CmdAdminSetHealth(player.character.Id, 100);
                            player.setup.CmdAdminSetHunger(player.character.Id, 100);
                            player.setup.CmdAdminSetThirst(player.character.Id, 100); */
                            player.Health = 100;
                            player.Hunger = 100;
                            player.Thirst = 100;
                        }
                        else
                        {
                            player.SetAdminService(true);
                            player.SendText(string.Format("<color={0}>Service admin activé !</color>", (object)"#8ec765"));

                            /*   if (player.setup.isFlying == true)
                                   player.SendText(" ");
                               else
                               {
                                   Nova.character.CmdAdminFly();
                               }
                               if (player.setup.isVanished == true) player.SendText(" ");
                               else
                               {
                                   player.setup.CmdAdminVanish();
                               }
                               player.setup.CmdAdminSetHealth(player.character.Id, 9999);
                               player.setup.CmdAdminSetHunger(player.character.Id, 9999);
                               player.setup.CmdAdminSetThirst(player.character.Id, 9999); */
                            player.Health = 9999;
                            player.Hunger = 9999;
                            player.Thirst = 9999;

                        }
                        player.serviceAdmin = !player.serviceAdmin;
                    }
                    else
                    {

                        player.SendText("<color=#e8472a>Vous n'êtes pas administrateur.</color>");

                    }
                });
                Player closestPlayer = player.GetClosestPlayer(true);

                adminmenu.AddTabLine("Menu Global", (ui) => player.ShowPanelUI(globalmenu)); player.ClosePanel(adminmenu);
                adminmenu.AddTabLine("Menu Administration", (ui) => player.ShowPanelUI(samenu));
                adminmenu.AddTabLine("Menu Soins", (ui) => player.ShowPanelUI(soinsmenu));
                adminmenu.AddTabLine("Menu Terrain", (ui) => player.ShowPanelUI(terrainmenu));
                adminmenu.AddTabLine("Menu Véhicule", (ui) => player.ShowPanelUI(vehiclesmenu));
                adminmenu.AddTabLine("Menu TP TERRAIN", (ui) => player.ShowPanelUI(spawnterrainmenu));
                //adminmenu.AddTabLine("Menu MESSAGE ADMIN", (ui) => player.ShowPanelUI(messagemenu));

                    globalmenu.AddTabLine("Rejoindre une entreprise", (ui) =>
                       {
                           player.ShowPanelUI(new UIPanel("Rejoindre une entreprise pour : " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("ID entreprise...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                               {
                                   int num = int.Parse(ui2.inputText);
                                   player.ClosePanel(ui);
                                   if (closestPlayer == null)
                                   {
                                       player.SendText("<color=green>Vous êtes désormais dans l'entreprise ayant pour ID " + num + "</color>");
                                       player.character.BizId = num;
                                   }
                                   else
                                   {
                                       player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " est désormais dans l'entreprise ayant pour ID " + num + "</color>");
                                       closestPlayer.SendText("<color=green>Vous êtes désormais dans l'entreprise ayant pour ID " + num + "</color>");
                                       player.character.BizId = num;
                                   }
                                   player.ClosePanel(ui2);

                               })));
                       });
                              /*  globalmenu.AddTabLine("Give de l'argent en poche", (ui) =>
                                {
                                    player.ShowPanelUI(new UIPanel("Give de l'argent en poche à " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Nombre d'argent en poche...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                                    {
                                        int num = int.Parse(ui2.inputText);
                                        player.setup.CmdAdminGiveMoney(closestPlayer == null ? player.character.Id : closestPlayer.character.Id, num);
                                        player.ClosePanel(ui);
                                        if (closestPlayer == null)
                                        {
                                            closestPlayer.SendText("<color=green>Vous venez de reçevoir " + num + "€ en banque </color>");
                                        }
                                        else
                                        {
                                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient de reçevoir " + num + "€ en poche</color>");

                                            closestPlayer.SendText("<color=green>Vous venez de reçevoir " + num + "€ en poche </color>");
                                        }
                                        player.ClosePanel(ui2);
                                    })));
                                });
                                 globalmenu.AddTabLine("Give de l'argent en banque", (ui) =>
                                {
                                    player.ShowPanelUI(new UIPanel("Give de l'argent en banque à " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Nombre d'argent en banque...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                                    {
                                        int num = int.Parse(ui2.inputText);
                                        player.setup.CmdAdminGiveBankMoney(closestPlayer == null ? player.character.Id : closestPlayer.character.Id, num);
                                        player.ClosePanel(ui);
                                        if (closestPlayer == null)
                                        {
                                            player.SendText("<color=green>Vous venez de reçevoir " + num + "€ en banque</color>");
                                        }
                                        else
                                        {
                                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient de reçevoir " + num + "€ en banque</color>");
                                            closestPlayer.SendText("<color=green>Vous venez de reçevoir " + num + "€ en banque</color>");
                                        }
                                        player.ClosePanel(ui2);
                                    })));
                                }); */
                    //     globalmenu.AddTabLine("Regarder l'inventaire du joueur le plus proche", (ui) => { player.setup.CmdAdminSeeInventory(closestPlayer == null ? player.character.Id : closestPlayer.character.Id); player.SendText("Vous espionnez désormais l'inventaire du joueur : " + closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()); });
                    globalmenu.AddTabLine("Définir numéro de téléphone", (ui) =>
                {
                    player.ShowPanelUI(new UIPanel("Définir le numéro de téléphone de : " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Numéro de téléphone...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                    {
                        string num = ui2.inputText;
                        if (closestPlayer == null)
                        {
                            player.character.PhoneNumber = num;
                            player.Save();
                            player.SendText("<color=green>Vous disposez désormais du numéro : </color>" + num);
                            player.ClosePanel(ui2);
                        }
                        else
                        {
                            closestPlayer.character.PhoneNumber = num;
                            closestPlayer.Save();
                            closestPlayer.SendText("<color=green>Vous disposez désormais du numéro :</color> " + num);
                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " dispose désormais du numéro : </color>" + num);
                            player.ClosePanel(ui2);
                        }
                        player.ClosePanel(ui);
                    })));
                });
                samenu.AddTabLine("Donner un rang administrateur", (ui) => {
                    if (player.account.adminLevel <= 9) { player.SendText("<color=red>Vous n'avez pas la permission !</color>"); }

                    if (closestPlayer != null)
                    {
                        player.ShowPanelUI(new UIPanel(string.Format("Définir rang administrateur de : " + closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Rang administrateur...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                        {
                            closestPlayer.account.adminLevel = int.Parse(ui2.inputText);
                            closestPlayer.Save();
                            player.ClosePanel(ui);
                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " est désormais administrateur de niveau</color> " + ui2.inputText);
                            closestPlayer.SendText("<color=green>Vous êtes désormais administrateur de niveau </color>" + ui2.inputText);
                            player.ClosePanel(ui2);
                        })));
                    }; });

                samenu.AddTabLine("Donner un mot de passe administrateur", (ui) => {
                    if (player.account.adminLevel <= 9) { player.SendText("<color=red>Vous n'avez pas la permission !</color>"); }

                    if (closestPlayer != null)
                    {
                        player.ShowPanelUI(new UIPanel(string.Format("Définir mot de passe administrateur de : " + closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Mot de passe administrateur...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                        {
                            closestPlayer.account.adminPin = ui2.inputText;
                            closestPlayer.Save();
                            player.ClosePanel(ui2);
                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " possède désormais le mot de passe </color>" + ui2.inputText);
                            closestPlayer.SendText("<color=green>Vous disposez désormais du mot de passe </color>" + ui2.inputText);
                        })));
                    }
                });
                    messagemenu.AddTabLine("Interpeller un joueur", (ui) =>
                    {

                        if (closestPlayer != null)
                        {
                            player.ShowPanelUI(new UIPanel(string.Format("Vous allez interpeller le joueur : " + closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Message...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                            {
                                closestPlayer.setup.TargetPlayClairon(0.5f);
                                //player.ClosePanel(ui2);
                                player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient d'être interpellé </color>" + ui2.inputText);
                                closestPlayer.SendText("<color=red>Un administrateur vous interpelle avec le message suivant : </color>" + ui2.inputText);
                            })));
                        }
                    });

                    messagemenu.AddTabLine("Admin besoin de vous", (ui) => {

                        if (closestPlayer != null)
                        {
                            player.ShowPanelUI(new UIPanel(string.Format("Vous allez interpeller le joueur : " + closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetText("Un Administrateur à besoin de vous ! Veuillez vous éloignez de l'action RP").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                            {
                                closestPlayer.setup.TargetPlayClairon(0.5f);
                               // player.ClosePanel(ui2);
                                player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient d'être interpellé </color>");
                                closestPlayer.SendText("<color=red>Un administrateur vous interpelle avec le message suivant : </color>" + "Un Administrateur à besoin de vous ! Veuillez vous éloignez de l'action RP");
                            })));
                        }
                    });
                    messagemenu.AddTabLine("Admin spec", (ui) => {

                        if (closestPlayer != null)
                        {
                            player.ShowPanelUI(new UIPanel(string.Format("Vous allez interpeller le joueur : " + closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetText("Un Administrateur est entrain de regarder de l'action RP, veuillez continuer votre RP comme si de rien n'était.").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                            {
                                closestPlayer.setup.TargetPlayClairon(0.5f);
                                // player.ClosePanel(ui2);
                                player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient d'être interpellé </color>");
                                closestPlayer.SendText("<color=red>Un administrateur vous interpelle avec le message suivant : </color>" + "Un Administrateur est entrain de regarder de l'action RP, veuillez continuer votre RP comme si de rien n'était.");
                            })));
                        }
                    });


                    soinsmenu.AddTabLine("Soigner la personne proche", (ui) => {

                if (closestPlayer != null)
                    {
                        closestPlayer.character.Health = 100;
                        player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient d'être soigné</color>");
                        closestPlayer.SendText("<color=green>Vous venez d'être soigné par un administrateur </color>");


                    }
                    else
                    {
                        player.character.Health = 100;
                        player.SendText("<color=green>Vous venez d'être soigné</color>");

                    }


                });
                    soinsmenu.AddTabLine("Nourrir/hydrater la personne proche", (ui) =>
                    {
                        if (closestPlayer != null)
                        {
                            closestPlayer.character.Hunger = 100;
                            closestPlayer.character.Thirst = 100;
                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient d'être rassasié</color>");
                            closestPlayer.SendText("<color=green>Vous venez d'être rassasié par un administrateur </color>");



                        }
                        else
                        {
                            player.character.Hunger = 100;
                            player.character.Thirst = 100;
                            player.SendText("<color=green>Vous venez d'être rassasié</color>");


                        }

                    });
                    soinsmenu.AddTabLine("Nourrir/hydrater pour l'éternité la personne proche", (ui) => {
                        if (closestPlayer != null)
                        {
                            closestPlayer.character.Hunger = 9999;
                            closestPlayer.character.Thirst = 9999;
                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " vient d'être rassasié pour l'éternité</color>");
                            closestPlayer.SendText("<color=green>Vous venez d'être rassasié pour l'éternité par un administrateur </color>");



                        }
                        else
                        {
                            player.character.Hunger = 9999;
                            player.character.Thirst = 9999;
                            player.SendText("<color=green>Vous venez d'être rassasié pour l'éternité</color>");

                           
                        }
                    });
                globalmenu.AddTabLine("Donner point permis", (ui) => {
                    player.ShowPanelUI(new UIPanel("Définir les points du permis : " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Nombre de points...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                    {
                        if (closestPlayer != null)
                        {
                            closestPlayer.character.PermisPoints = int.Parse(ui2.inputText);
                            closestPlayer.Save();
                            player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " possède désormais " + ui2.inputText + " sur le permis B </color>");
                            closestPlayer.SendText("<color=green>Vous disposez désormais de " + ui2.inputText + " sur le permis B</color>");
                            player.ClosePanel(ui2);
                        }
                        else
                        {
                            player.character.PermisPoints = int.Parse(ui2.inputText);
                            player.Save();
                            player.SendText("<color=green>Vous disposez désormais de " + ui2.inputText + " sur le permis B</color>");
                            player.ClosePanel(ui2);
                        }
                    })));
                });

                globalmenu.AddTabLine("Donner permis", (ui) => {

                    if (closestPlayer != null)
                    {
                        closestPlayer.character.PermisB = true;
                        closestPlayer.Save();
                        player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " possède désormais le permis B</color>");
                        closestPlayer.SendText("<color=green>Vous disposez désormais du permis B</color>");
                    }
                    else
                    {
                        player.character.PermisB = true;
                        player.Save();
                        player.SendText("<color=green>Vous disposez désormais le permis B</color>");

                    }
                });
                    globalmenu.AddTabLine("Définir prénom RP du joueur", (ui) =>
                    {
                        player.ShowPanelUI(new UIPanel("Définir le prénom RP de : " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Prénom RP...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                        {
                            string num = ui2.inputText;
                            if (closestPlayer == null)
                            {
                                player.character.Firstname = num;
                                player.Save();
                                player.SendText("<color=green>Vous disposez désormais du prénom RP : </color>" + num);
                                player.ClosePanel(ui2);
                            }
                            else
                            {
                                closestPlayer.character.Firstname = num;
                                closestPlayer.Save();
                                closestPlayer.SendText("<color=green>Vous disposez désormais du prénom RP :</color> " + num);
                                player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " dispose désormais du prénom RP : </color>" + num);
                                player.ClosePanel(ui2);
                            }
                            player.ClosePanel(ui);
                        })));
                    });
                    globalmenu.AddTabLine("Définir nom de famille RP du joueur", (ui) =>
                    {
                        player.ShowPanelUI(new UIPanel("Définir le nom de famille RP de : " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Nom de famille RP...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                        {
                            string num = ui2.inputText;
                            if (closestPlayer == null)
                            {
                                player.character.Lastname = num;
                                player.Save();
                                player.SendText("<color=green>Vous disposez désormais du nom de famille RP : </color>" + num);
                                player.ClosePanel(ui2);
                            }
                            else
                            {
                                closestPlayer.character.Lastname = num;
                                closestPlayer.Save();
                                closestPlayer.SendText("<color=green>Vous disposez désormais du nom de famille RP :</color> " + num);
                                player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " dispose désormais du nom de famille RP : </color>" + num);
                                player.ClosePanel(ui2);
                            }
                            player.ClosePanel(ui);
                        })));
                    });

                    globalmenu.AddTabLine("Donner Level", (ui) => {
                        if (player.account.adminLevel == 10) { player.SendText("<color=red>Vous n'avez pas la permission !</color>"); }
                        player.ShowPanelUI(new UIPanel("Définir le niveau : " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Niveau...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                        {
                            if (closestPlayer != null)
                            {
                                closestPlayer.character.Level = int.Parse(ui2.inputText);
                                closestPlayer.Save();
                                player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " possède désormais le niveau " + ui2.inputText + " </color>");
                                closestPlayer.SendText("<color=green>Vous disposez désormais du niveau " + ui2.inputText + " </color>");
                                player.ClosePanel(ui2);
                            }
                            else
                            {
                                player.character.Level = int.Parse(ui2.inputText);
                                player.Save();
                                player.SendText("<color=green>Vous disposez désormais du niveau " + ui2.inputText + " </color>");
                                player.ClosePanel(ui2);
                            }
                        })));
                    });

                    globalmenu.AddTabLine("Donner XP", (ui) => {
                        if (player.account.adminLevel == 10) { player.SendText("<color=red>Vous n'avez pas la permission !</color>"); }
                        player.ShowPanelUI(new UIPanel("Définir le nombre d'expérience : " + (closestPlayer == null ? player.GetFullName() : closestPlayer.GetFullName()), (UIPanel.PanelType)1).SetInputPlaceholder("Nombre d'expérience...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                        {
                            if (closestPlayer != null)
                            {
                                closestPlayer.character.XP = int.Parse(ui2.inputText);
                                closestPlayer.Save();
                                player.SendText("<color=green>Le joueur " + closestPlayer.GetFullName() + " possède désormais " + ui2.inputText + " d'expérience </color>");
                                closestPlayer.SendText("<color=green>Vous disposez désormais de " + ui2.inputText + " d'expérience</color>");
                                player.ClosePanel(ui2);
                            }
                            else
                            {
                                player.character.XP = int.Parse(ui2.inputText);
                                player.Save();
                                player.SendText("<color=green>Vous disposez désormais de " + ui2.inputText + " d'expérience</color>");
                                player.ClosePanel(ui2);
                            }
                        })));
                    });


                    terrainmenu.AddTabLine("Définir nouveau propriétaire", (ui) =>
                {
                    if (player.account.adminLevel >= 8) { player.SendText("<color=red>Vous n'avez pas la permission !</color>"); }
                    player.ShowPanelUI(new UIPanel("Définir nouveau propriétaire", (UIPanel.PanelType)1).SetInputPlaceholder("ID propriétaire...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                    {
                        int num = int.Parse(ui2.inputText);
                        player.ClosePanel(ui);

                        if (player.setup.areaId > 0)
                        {
                            LifeArea area = Nova.a.GetAreaById(player.setup.areaId);

                            area.permissions = new Permissions()
                            {
                                owner = new Entity() { characterId = num },
                                coOwners = new List<Entity>()
                            };


                            area.Save();

                            player.SendText("<color=green>Le propriétaire du terrain vient d'être modifié !</color>");
                            player.ClosePanel(ui2);

                        }

                    })));
                });
                terrainmenu.AddTabLine("Définir prix de vente", (ui) =>
                {
                    if (player.account.adminLevel >= 8) { player.SendText("<color=red>Vous n'avez pas la permission !</color>"); }
                    player.ShowPanelUI(new UIPanel("Définir prix de vente", (UIPanel.PanelType)1).SetInputPlaceholder("prix de vente...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                    {
                        int num = int.Parse(ui2.inputText);
                        player.ClosePanel(ui);

                        if (player.setup.areaId > 0)
                        {
                            LifeArea area = Nova.a.GetAreaById(player.setup.areaId);

                            area.price = num;

                            area.Save();

                            player.SendText("<color=green>Le prix du terrain vient d'être modifié !</color>");
                            player.ClosePanel(ui2);


                        }

                    })));
                });
                terrainmenu.AddTabLine("Définir prix de location", (ui) =>
                {
                    if (player.account.adminLevel >= 8) { player.SendText("<color=red>Vous n'avez pas la permission !</color>"); }
                    player.ShowPanelUI(new UIPanel("Définir prix de location", (UIPanel.PanelType)1).SetInputPlaceholder("prix de location...").AddButton("Fermer", (Action<UIPanel>)(ui2 => player.ClosePanel(ui2))).AddButton("Valider", (Action<UIPanel>)(ui2 =>
                    {
                        int num = int.Parse(ui2.inputText);
                        player.ClosePanel(ui);

                        if (player.setup.areaId > 0)
                        {
                            LifeArea area = Nova.a.GetAreaById(player.setup.areaId);

                            area.rentPrice = num;

                            if (area.rentPrice > 0)
                            {
                                area.isRentable = true;
                            }
                            else
                            {
                                area.isRentable = false;
                            }

                            area.Save();

                            player.SendText("<color=green>Le prix de location vient d'être  modifié !</color>");
                            player.ClosePanel(ui2);
                        }

                    })));
                });

                    foreach (var area in Nova.server.areas.areas)
                    {
                        LifeArea areatwo = Nova.a.GetAreaById(area.id);
                        if (areatwo.permissions.owner != null)
                        {

                            spawnterrainmenu.AddTabLine($"Terrain n°{area.id}", (ui) =>
                        {
                            player.setup.TargetSetPosition(area.spawn);

                        });
                        }
                    }

                    
                  

                    vehiclesmenu.AddTabLine("Avancer le véhicule", (ui) =>
                    {


                        if (player.IsAdmin)
                        {
                            uint vehicleId = player.GetVehicleId();

                            if (vehicleId > 0)
                            {
                                NetworkServer.spawned[vehicleId].GetComponent<Vehicle>().RpcAddPosition(NetworkServer.spawned[vehicleId].transform.forward);

                                NetworkServer.spawned[vehicleId].transform.position += NetworkServer.spawned[vehicleId].transform.forward;
                                player.SendText("<color=green>Le véhicule vient d'être déplacé vers l'avant</color>");
                            }
                            else
                            {
                                player.SendText("<color=red>Vous n'êtes pas dans un véhicule.</color>");
                            }
                        }
                    
                    });

                    vehiclesmenu.AddTabLine("Reculer le véhicule", (ui) =>
                    {

                        if (player.IsAdmin)
                        {
                            uint vehicleId = player.GetVehicleId();

                            if (vehicleId > 0)
                            {
                                NetworkServer.spawned[vehicleId].GetComponent<Vehicle>().RpcAddPosition(-NetworkServer.spawned[vehicleId].transform.forward);

                                NetworkServer.spawned[vehicleId].transform.position -= NetworkServer.spawned[vehicleId].transform.forward;
                                player.SendText("<color=green>Le véhicule vient d'être déplacé vers l'arrière</color>");
                            }
                            else
                            {
                                player.SendText("<color=red>Vous n'êtes pas dans un véhicule.</color>");
                            }
                        }                    

                    });
                    vehiclesmenu.AddTabLine("Aller à droite avec le véhicule", (ui) =>
                    {

                        if (player.IsAdmin)
                        {
                            uint vehicleId = player.GetVehicleId();

                            if (vehicleId > 0)
                            {
                                NetworkServer.spawned[vehicleId].GetComponent<Vehicle>().RpcAddPosition(NetworkServer.spawned[vehicleId].transform.right);

                                NetworkServer.spawned[vehicleId].transform.position += NetworkServer.spawned[vehicleId].transform.right;
                                player.SendText("<color=green>Le véhicule vient d'être déplacé vers la droite</color>");
                            }
                            else
                            {
                                player.SendText("<color=red>Vous n'êtes pas dans un véhicule.</color>");
                            }
                        }                    

                    });
                    vehiclesmenu.AddTabLine("Aller à gauche avec le véhicule", (ui) =>
                    {

                        if (player.IsAdmin)
                        {
                            uint vehicleId = player.GetVehicleId();

                            if (vehicleId > 0)
                            {
                                NetworkServer.spawned[vehicleId].GetComponent<Vehicle>().RpcAddPosition(-NetworkServer.spawned[vehicleId].transform.right);

                                NetworkServer.spawned[vehicleId].transform.position -= NetworkServer.spawned[vehicleId].transform.right;
                                player.SendText("<color=green>Le véhicule vient d'être déplacé vers la gauche</color>");
                            } else
                            {
                                player.SendText("<color=red>Vous n'êtes pas dans un véhicule.</color>");
                            }
                        }                    

                    });
                    vehiclesmenu.AddTabLine("Retourner le véhicule", (ui) =>
                    {
                        if (player.IsAdmin)
                        {
                            uint vehicleId = player.GetVehicleId();

                            if (vehicleId > 0)
                            {
                                NetworkServer.spawned[vehicleId].GetComponent<Vehicle>().RpcFlip();

                                NetworkServer.spawned[vehicleId].transform.rotation = Quaternion.Euler(Vector3.zero);
                                NetworkServer.spawned[vehicleId].transform.position += Vector3.up * 2f;
                                player.SendText("<color=green>Le véhicule vient d'être retourné</color>");
                            }
                            else
                            {
                                player.SendText("<color=red>Vous n'êtes pas dans un véhicule.</color>");
                            }
                        }
                    });
                    vehiclesmenu.AddTabLine("Réparer le véhicule", (ui) =>
                    {

                        Vehicle vehicle = player.GetClosestVehicle();

                        if (vehicle != null)
                        {
                            vehicle.Repair();
                            player.SendText("<color=green> Le véhicule vien d'être réparé avec succès.</color>");
                        }
                        else
                        {
                            player.SendText("<color=red>Aucun véhicule à proximité</color>");
                        }                               
                    

                    });
                    vehiclesmenu.AddTabLine("Remplir le véhicule", (ui) =>
                    {
                        Vehicle vehicle = player.GetClosestVehicle();

                        if (vehicle != null)
                        {
                            vehicle.fuel = 100f;
                            player.SendText("<color=green>Le véhicule vient d'être rempli en essence</color>");
                        }   else
                        {
                            player.SendText("<color=red>Aucun véhicule à proximité</color>");
                        }                         


                    });
                    vehiclesmenu.AddTabLine("Ranger le véhicule", (ui) =>
                    {

                        Vehicle vehicle = player.GetClosestVehicle();

                        if (vehicle != null && vehicle.vehicleDbId > 0)
                        {
                            Nova.v.StowVehicle(vehicle.vehicleDbId);
                            player.SendText("<color=green>Le véhicule vient d'être ranger au garage</color>");
                        }
                        else
                        {
                            player.SendText("<color=red>Aucun véhicule à proximité</color>");
                        }                                                       
                    });

                    adminmenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    adminmenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    globalmenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    globalmenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    samenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    samenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    soinsmenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    soinsmenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    terrainmenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    terrainmenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    vehiclesmenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    vehiclesmenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    spawnterrainmenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    spawnterrainmenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    messagemenu.AddButton("Fermer", (ui) => { player.ClosePanel(ui); });
                    messagemenu.AddButton("Sélectionner", (Action<UIPanel>)(ui1 => ui1.SelectTab()));

                    // globalmenu.AddButton("Retour", (ui) => { player.ShowPanelUI(adminmenu); });
                    // samenu.AddButton("Retour", (ui) => { player.ShowPanelUI(adminmenu); });
                    //  soinsmenu.AddButton("Retour", (ui) => { player.ShowPanelUI(adminmenu); });
                    //terrainmenu.AddButton("Retour", (ui) => { player.ShowPanelUI(adminmenu); 

                    player.ShowPanelUI(adminmenu);
                }
                else
                    player.SendText("<color=red>Vous n'êtes pas administrateur !</color>");
            }));
            schatCommand1.Register();
            schatCommand3.Register();
        }

        public override void OnPlayerInput(Player player, KeyCode keyCode, bool onUI)
        {
            base.OnPlayerInput(player, keyCode, onUI);

            if (keyCode == KeyCode.Quote && !onUI)
            {
                if (player.IsAdmin)
                {

                    if (!player.isAuthAdmin)
                    {
                        player.SendText("<color=#e8472a>Vous n'êtes pas authentifié.</color>");
                    }

                    if (player.setup.isAdminService == true)
                    {
                        player.SetAdminService(false);
                        player.SendText(string.Format("<color={0}>Service admin désactivé !</color>", (object)"#e8472a"));

                        /*  if (player.setup.isFlying == false) player.SendText(" ");
                          else
                          {
                              Nova.character.CmdAdminFly();
                          }
                          if (player.setup.isVanished == false) player.SendText(" ");
                          else
                          {
                              player.setup.CmdAdminVanish();
                          }
                        player.setup.CmdAdminSetHealth(player.character.Id, 100);
                        player.setup.CmdAdminSetHunger(player.character.Id, 100);
                        player.setup.CmdAdminSetThirst(player.character.Id, 100); */
                        player.Health = 100;
                        player.Hunger = 100;
                        player.Thirst = 100;
                    }
                    else
                    {
                        player.SetAdminService(true);
                        player.SendText(string.Format("<color={0}>Service admin activé !</color>", (object)"#8ec765"));

                     /*   if (player.setup.isFlying == true)
                            player.SendText(" ");
                        else
                        {
                            Nova.character.CmdAdminFly();
                        }
                        if (player.setup.isVanished == true) player.SendText(" ");
                        else
                        {
                            player.setup.CmdAdminVanish();
                        }
                        player.setup.CmdAdminSetHealth(player.character.Id, 9999);
                        player.setup.CmdAdminSetHunger(player.character.Id, 9999);
                        player.setup.CmdAdminSetThirst(player.character.Id, 9999); */
                        player.Health = 9999;
                        player.Hunger = 9999;
                        player.Thirst = 9999;

                    }
                    player.serviceAdmin = !player.serviceAdmin;
                } else
                {

                    player.SendText("<color=#e8472a>Vous n'êtes pas administrateur.</color>");

                }




            }
          
        
        }
         }
    }


