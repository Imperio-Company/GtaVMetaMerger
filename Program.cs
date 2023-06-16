using Microsoft.Extensions.FileSystemGlobbing;
using System.Xml;

namespace PedsMetaMerger
{
    internal class Program
    {
        static XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true
        };
        static void Main(string[] args)
        {
            Boolean continuar = true;
            Boolean continuar2 = true;

            if (!Directory.Exists("output/vehicles"))
            {
                Directory.CreateDirectory("output/vehicles");
            }

            if (!Directory.Exists("output/peds"))
            {
                Directory.CreateDirectory("output/peds");
            }

            if (!Directory.Exists("output/weapons"))
            {
                Directory.CreateDirectory("output/weapons");
            }

            do
            {
                Console.WriteLine("1. Peds");
                Console.WriteLine("2. Vehicles");
                Console.WriteLine("3. Weapons");
                Console.WriteLine("0. Salir");
                switch (Console.ReadLine())
                {
                    case "1":
                        do
                        {
                            Console.WriteLine("1. Merge peds.meta");
                            Console.WriteLine("2. Merge stream");
                            Console.WriteLine("3. Merge todo");
                            Console.WriteLine("4. Extraer ped names");
                            Console.WriteLine("0. Atras");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    MergePeds();
                                    break;
                                case "2":
                                    MergePedStream();
                                    break;
                                case "3":
                                    MergePeds();
                                    MergePedStream();
                                    break;
                                case "4":
                                    ExtractPedNames();
                                    break;
                                case "0":
                                    continuar2 = false;
                                    break;
                                default:
                                    continuar2 = true;
                                    break;
                            }
                        } while (continuar2);
                        break;
                    case "2":
                        do
                        {
                            Console.WriteLine("1. Merge vehicles.meta");
                            Console.WriteLine("2. Merge carcols.meta");
                            Console.WriteLine("3. Merge carvariations.meta");
                            Console.WriteLine("4. Merge handling.meta");
                            Console.WriteLine("5. Merge vehiclelayouts.meta");
                            Console.WriteLine("6. Merge stream");
                            Console.WriteLine("7. Merge todos");
                            Console.WriteLine("8. Extraer vehicle names");
                            Console.WriteLine("0. Atras");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    MergeVehicles();
                                    break;
                                case "2":
                                    MergeCarcols();
                                    break;
                                case "3":
                                    MergeCarvariations();
                                    break;
                                case "4":
                                    MergeHandling();
                                    break;
                                case "5":
                                    MergeVehicleLayouts();
                                    break;
                                case "6":
                                    MergeVehicleStream();
                                    break;
                                case "7":
                                    MergeVehicles();
                                    MergeCarcols();
                                    MergeCarvariations();
                                    MergeHandling();
                                    MergeVehicleLayouts();
                                    MergeVehicleStream();
                                    break;
                                case "8":
                                    ExtractVehicleNames();
                                    break;
                                case "0":
                                    continuar2 = false;
                                    break;
                                default:
                                    continuar2 = true;
                                    break;
                            }
                        } while (continuar2);
                        break;
                    case "3":
                        do
                        {
                            Console.WriteLine("1. Merge weapons.meta/weapon_X.meta");
                            Console.WriteLine("2. Merge weaponanimations.meta");
                            Console.WriteLine("3. Merge weaponarchetypes.meta");
                            Console.WriteLine("4. Merge weaponcomponents.meta");
                            Console.WriteLine("5. Merge pedpersonality.meta");
                            Console.WriteLine("6. Merge pickups.meta");
                            Console.WriteLine("7. Merge ptfxassetinfo.meta");
                            Console.WriteLine("8. Merge stream");
                            Console.WriteLine("9. Merge todo");
                            Console.WriteLine("10. Extraer weapon names");
                            Console.WriteLine("0. Atras");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    MergeWeapons();
                                    break;
                                case "2":
                                    MergeWeaponAnimations();
                                    break;
                                case "3":
                                    MergeWeaponArchetypes();
                                    break;
                                case "4":
                                    MergeWeaponComponents();
                                    break;
                                case "5":
                                    MergePedPersonality();
                                    break;
                                case "6":
                                    MergePickups();
                                    break;
                                case "7":
                                    MergePtfxAssetInfo();
                                    break;
                                case "8":
                                    MergeWeaponStream();
                                    break;
                                case "9":
                                    MergeWeapons();
                                    MergeWeaponAnimations();
                                    MergeWeaponArchetypes();
                                    MergeWeaponComponents();
                                    MergePedPersonality();
                                    MergePickups();
                                    MergePtfxAssetInfo();
                                    MergeWeaponStream();
                                    break;
                                case "10":
                                    ExtractWeaponNames();
                                    break;
                                case "0":
                                    continuar2 = false;
                                    break;
                                default:
                                    continuar2 = true;
                                    break;
                            }
                        } while (continuar2);
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        continuar = true;
                        break;
                }
            } while (continuar);
        }

        private static void ExtractWeaponNames()
        {
            if (!File.Exists("output/weapons/weapons.meta"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("output/weapons/weapons.meta");
                XmlNodeList nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/Infos/Item/Infos/Item[type=\"CWeaponInfo\"]");
                StreamWriter salida = File.CreateText("output/weapons/WeaponNames.txt");

                for (int i = 0; i < nodes.Count; i++)
                {
                    salida.WriteLine(nodes[i].SelectSingleNode("Name").InnerText);
                }
                salida.Flush();
                salida.Close();
            }
        }

        private static void MergePtfxAssetInfo()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CPtFxAssetInfoMgr = nuevo.CreateElement("CPtFxAssetInfoMgr");
            XmlNode ptfxAssetDependencyInfos = nuevo.CreateElement("ptfxAssetDependencyInfos");
            XmlWriter writer = XmlWriter.Create("output/weapons/ptfxassetinfo.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/ptfxassetinfo.meta");
            String ruta = PedirRuta();

            CPtFxAssetInfoMgr.AppendChild(ptfxAssetDependencyInfos);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CPtFxAssetInfoMgr/ptfxAssetDependencyInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = ptfxAssetDependencyInfos.OwnerDocument.ImportNode(nodes[i], true);
                    ptfxAssetDependencyInfos.AppendChild(node);
                }
            }
            CPtFxAssetInfoMgr.WriteTo(writer);
            writer.Close();
        }

        private static void MergePickups()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CPickupDataManager = nuevo.CreateElement("CPickupDataManager");
            XmlNode pickupData = nuevo.CreateElement("pickupData");
            XmlNode actionData = nuevo.CreateElement("actionData");
            XmlNode rewardData = nuevo.CreateElement("rewardData");
            XmlWriter writer = XmlWriter.Create("output/weapons/pickups.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/pickups.meta");
            String ruta = PedirRuta();

            CPickupDataManager.AppendChild(pickupData);
            CPickupDataManager.AppendChild(actionData);
            CPickupDataManager.AppendChild(rewardData);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CPickupDataManager/pickupData/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = pickupData.OwnerDocument.ImportNode(nodes[i], true);
                    pickupData.AppendChild(node);
                }
                
                nodes = xmlDocument.SelectNodes("/CPickupDataManager/actionData/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = actionData.OwnerDocument.ImportNode(nodes[i], true);
                    actionData.AppendChild(node);
                }
                
                nodes = xmlDocument.SelectNodes("/CPickupDataManager/rewardData/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = rewardData.OwnerDocument.ImportNode(nodes[i], true);
                    rewardData.AppendChild(node);
                }
            }
            CPickupDataManager.WriteTo(writer);
            writer.Close();
        }

        private static void MergeWeaponStream()
        {
            if (!Directory.Exists("output/weapons/stream"))
            {
                Directory.CreateDirectory("output/weapons/stream");
            }

            String ruta = PedirRuta();

            foreach (String path in Directory.GetDirectories(ruta))
            {
                foreach (FileInfo file in new DirectoryInfo(Path.Combine(path, "stream")).EnumerateFiles())
                {
                    file.CopyTo(Path.Combine("output/weapons/stream", file.Name), true);
                }
            }
        }

        private static void MergePedPersonality()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CPedModelInfo__PersonalityDataList = nuevo.CreateElement("CPedModelInfo__PersonalityDataList");
            //MovementModeUnholsterData
            XmlNode MovementModeUnholsterData = nuevo.CreateElement("MovementModeUnholsterData");
            //UNHOLSTER_UNARMED
            XmlNode ItemUnholsterUnarmed = nuevo.CreateElement("Item");
            XmlNode NameUnholsterUnarmed = nuevo.CreateElement("Name");
            XmlNode UnholsterUnarmedUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode ItemUnarmedUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsUnarmedUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipUnarmedUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hMeleeUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item1hUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode ItemMiniUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsMiniUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipMiniUnholsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_2H_MELEE
            XmlNode ItemUnholster2hMelee = nuevo.CreateElement("Item");
            XmlNode NameUnholster2hMelee = nuevo.CreateElement("Name");
            XmlNode Unholster2hMeleeUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode Item2hMeleeHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hMeleeHolster2hMeleeUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeHolster2hMeleeUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeHolster2hMeleeUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hMeleeHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hMeleeHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hMeleeHolsterMiniUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeHolsterMiniUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeHolsterMiniUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_1H
            XmlNode ItemUnholster1h = nuevo.CreateElement("Item");
            XmlNode NameUnholster1h = nuevo.CreateElement("Name");
            XmlNode Unholster1hUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode Item1hHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item1hHolster2hMeleeUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hHolster2hMeleeUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hHolster2hMeleeUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item1hHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item1hHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item1hHolsterMiniUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hHolsterMiniUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hHolsterMiniUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_2H
            XmlNode ItemUnholster2h = nuevo.CreateElement("Item");
            XmlNode NameUnholster2h = nuevo.CreateElement("Name");
            XmlNode Unholster2hUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode Item2hHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hHolster2hMeleeUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hHolster2hMeleeUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hHolster2hMeleeUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hHolsterMiniUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hHolsterMiniUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hHolsterMiniUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_MINIGUN
            XmlNode ItemUnholsterMinigun = nuevo.CreateElement("Item");
            XmlNode NameUnholsterMinigun = nuevo.CreateElement("Name");
            XmlNode UnholsterMinigunUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode ItemMiniHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsMiniHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipMiniHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode ItemMiniHolster2hMeleeUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsMiniHolster2hMeleeUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipMiniHolster2hMeleeUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode ItemMiniHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsMiniHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipMiniHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode ItemMiniHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsMiniHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipMiniHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_UNARMED_STEALTH
            XmlNode ItemUnholsterUnarmedStealth = nuevo.CreateElement("Item");
            XmlNode NameUnholsterUnarmedStealth = nuevo.CreateElement("Name");
            XmlNode UnholsterUnarmedStealthUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode ItemUnarmedStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsUnarmedStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipUnarmedStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode ItemUnarmedStealthHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsUnarmedStealthHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipUnarmedStealthHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode ItemUnarmedStealthHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsUnarmedStealthHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipUnarmedStealthHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode ItemUnarmedStealthHolsterMiniUnholsterClips = nuevo.CreateElement("Item");
            XmlNode WeaponsUnarmedStealthHolsterMiniUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode ClipUnarmedStealthHolsterMiniUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_2H_MELEE_STEALTH
            XmlNode ItemUnholster2hMeleeStealth = nuevo.CreateElement("Item");
            XmlNode NameUnholster2hMeleeStealth = nuevo.CreateElement("Name");
            XmlNode Unholster2hMeleeStealthUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode Item2hMeleeStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hMeleeStealthHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeStealthHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeStealthHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hMeleeStealthHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hMeleeStealthHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hMeleeStealthHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_1H_STEALTH
            XmlNode ItemUnholster1hStealth = nuevo.CreateElement("Item");
            XmlNode NameUnholster1hStealth = nuevo.CreateElement("Name");
            XmlNode Unholster1hStealthUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode Item1hStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item1hStealthHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hStealthHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hStealthHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item1hStealthHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons1hStealthHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip1hStealthHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            //UNHOLSTER_2H_STEALTH
            XmlNode ItemUnholster2hStealth = nuevo.CreateElement("Item");
            XmlNode NameUnholster2hStealth = nuevo.CreateElement("Name");
            XmlNode Unholster2hStealthUnholsterClips = nuevo.CreateElement("UnholsterClips");
            XmlNode Item2hStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hStealthHolsterUnarmedUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hStealthHolster1hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hStealthHolster1hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hStealthHolster1hUnholsterClips = nuevo.CreateElement("Clip");
            XmlNode Item2hStealthHolster2hUnholsterClips = nuevo.CreateElement("Item");
            XmlNode Weapons2hStealthHolster2hUnholsterClips = nuevo.CreateElement("Weapons");
            XmlNode Clip2hStealthHolster2hUnholsterClips = nuevo.CreateElement("Clip");
            //MovementModes
            XmlNode MovementModes = nuevo.CreateElement("MovementModes");
            //DEFAULT_ACTION
            XmlNode ItemDefaultAction = nuevo.CreateElement("Item");
            XmlNode NameDefaultAction = nuevo.CreateElement("Name");
            XmlNode DefaultActionMovementNodes = nuevo.CreateElement("MovementNodes");
            XmlNode ItemDefaultActionNormal = nuevo.CreateElement("Item");
            XmlNode ItemDefaultActionStealth = nuevo.CreateElement("Item");
            //MP_FEMALE_ACTION
            XmlNode ItemMpFemaleAction = nuevo.CreateElement("Item");
            XmlNode NameMpFemaleAction = nuevo.CreateElement("Name");
            XmlNode MpFemaleActionMovementNodes = nuevo.CreateElement("MovementNodes");
            XmlNode ItemMpFemaleActionNormal = nuevo.CreateElement("Item");
            XmlNode ItemMpFemaleActionStealth = nuevo.CreateElement("Item");
            //MICHAEL_ACTION
            XmlNode ItemMichaelAction = nuevo.CreateElement("Item");
            XmlNode NameMichaelAction = nuevo.CreateElement("Name");
            XmlNode MichaelActionMovementNodes = nuevo.CreateElement("MovementNodes");
            XmlNode ItemMichaelActionNormal = nuevo.CreateElement("Item");
            XmlNode ItemMichaelActionStealth = nuevo.CreateElement("Item");
            //FRANKLIN_ACTION
            XmlNode ItemFranklinAction = nuevo.CreateElement("Item");
            XmlNode NameFranklinAction = nuevo.CreateElement("Name");
            XmlNode FranklinActionMovementNodes = nuevo.CreateElement("MovementNodes");
            XmlNode ItemFranklinActionNormal = nuevo.CreateElement("Item");
            XmlNode ItemFranklinActionStealth = nuevo.CreateElement("Item");
            //TREVOR_ACTION
            XmlNode ItemTrevorAction = nuevo.CreateElement("Item");
            XmlNode NameTrevorAction = nuevo.CreateElement("Name");
            XmlNode TrevorActionMovementNodes = nuevo.CreateElement("MovementNodes");
            XmlNode ItemTrevorActionNormal = nuevo.CreateElement("Item");
            XmlNode ItemTrevorActionStealth = nuevo.CreateElement("Item");
            //end xml
            XmlWriter writer = XmlWriter.Create("output/weapons/pedpersonality.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/pedpersonality.meta");
            String ruta = PedirRuta();

            //MovementModeUnholsterData
            CPedModelInfo__PersonalityDataList.AppendChild(MovementModeUnholsterData);
            //UNHOLSTER_UNARMED
            MovementModeUnholsterData.AppendChild(ItemUnholsterUnarmed);
            NameUnholsterUnarmed.Value = "UNHOLSTER_UNARMED";
            ItemUnholsterUnarmed.AppendChild(NameUnholsterUnarmed);
            ItemUnholsterUnarmed.AppendChild(UnholsterUnarmedUnholsterClips);
            UnholsterUnarmedUnholsterClips.AppendChild(ItemUnarmedUnholsterUnarmedUnholsterClips);
            ItemUnarmedUnholsterUnarmedUnholsterClips.AppendChild(WeaponsUnarmedUnholsterUnarmedUnholsterClips);
            ClipUnarmedUnholsterUnarmedUnholsterClips.Value = "unarmed_holster_unarmed";
            ItemUnarmedUnholsterUnarmedUnholsterClips.AppendChild(ClipUnarmedUnholsterUnarmedUnholsterClips);
            UnholsterUnarmedUnholsterClips.AppendChild(Item2hMeleeUnholsterUnarmedUnholsterClips);
            Item2hMeleeUnholsterUnarmedUnholsterClips.AppendChild(Weapons2hMeleeUnholsterUnarmedUnholsterClips);
            Clip2hMeleeUnholsterUnarmedUnholsterClips.Value = "unarmed_holster_2h_melee";
            Item2hMeleeUnholsterUnarmedUnholsterClips.AppendChild(Clip2hMeleeUnholsterUnarmedUnholsterClips);
            UnholsterUnarmedUnholsterClips.AppendChild(Item1hUnholsterUnarmedUnholsterClips);
            Item1hUnholsterUnarmedUnholsterClips.AppendChild(Weapons1hUnholsterUnarmedUnholsterClips);
            Clip1hUnholsterUnarmedUnholsterClips.Value = "unarmed_holster_1h";
            Item1hUnholsterUnarmedUnholsterClips.AppendChild(Clip1hUnholsterUnarmedUnholsterClips);
            UnholsterUnarmedUnholsterClips.AppendChild(Item2hUnholsterUnarmedUnholsterClips);
            Item2hUnholsterUnarmedUnholsterClips.AppendChild(Weapons2hUnholsterUnarmedUnholsterClips);
            Clip2hUnholsterUnarmedUnholsterClips.Value = "unarmed_holster_2h";
            Item2hUnholsterUnarmedUnholsterClips.AppendChild(Clip2hUnholsterUnarmedUnholsterClips);
            UnholsterUnarmedUnholsterClips.AppendChild(ItemMiniUnholsterUnarmedUnholsterClips);
            ItemMiniUnholsterUnarmedUnholsterClips.AppendChild(WeaponsMiniUnholsterUnarmedUnholsterClips);
            ClipMiniUnholsterUnarmedUnholsterClips.Value = "unarmed_holster_mini";
            ItemMiniUnholsterUnarmedUnholsterClips.AppendChild(ClipMiniUnholsterUnarmedUnholsterClips);
            //UNHOLSTER_2H_MELEE
            MovementModeUnholsterData.AppendChild(ItemUnholster2hMelee);
            NameUnholster2hMelee.Value = "UNHOLSTER_2H_MELEE";
            ItemUnholster2hMelee.AppendChild(NameUnholster2hMelee);
            ItemUnholster2hMelee.AppendChild(Unholster2hMeleeUnholsterClips);
            Unholster2hMeleeUnholsterClips.AppendChild(Item2hMeleeHolsterUnarmedUnholsterClips);
            Item2hMeleeHolsterUnarmedUnholsterClips.AppendChild(Weapons2hMeleeHolsterUnarmedUnholsterClips);
            Clip2hMeleeHolsterUnarmedUnholsterClips.Value = "unarmed_holster_mini";
            Item2hMeleeHolsterUnarmedUnholsterClips.AppendChild(Clip2hMeleeHolsterUnarmedUnholsterClips);
            Unholster2hMeleeUnholsterClips.AppendChild(Item2hMeleeHolster2hMeleeUnholsterClips);
            Item2hMeleeHolster2hMeleeUnholsterClips.AppendChild(Weapons2hMeleeHolster2hMeleeUnholsterClips);
            Clip2hMeleeHolster2hMeleeUnholsterClips.Value = "2h_melee_holster_2h_melee";
            Item2hMeleeHolster2hMeleeUnholsterClips.AppendChild(Clip2hMeleeHolster2hMeleeUnholsterClips);
            Unholster2hMeleeUnholsterClips.AppendChild(Item2hMeleeHolster1hUnholsterClips);
            Item2hMeleeHolster1hUnholsterClips.AppendChild(Weapons2hMeleeHolster1hUnholsterClips);
            Clip2hMeleeHolster1hUnholsterClips.Value = "2h_melee_holster_1h";
            Item2hMeleeHolster1hUnholsterClips.AppendChild(Clip2hMeleeHolster1hUnholsterClips);
            Unholster2hMeleeUnholsterClips.AppendChild(Item2hMeleeHolster2hUnholsterClips);
            Item2hMeleeHolster2hUnholsterClips.AppendChild(Weapons2hMeleeHolster2hUnholsterClips);
            Clip2hMeleeHolster2hUnholsterClips.Value = "2h_melee_holster_2h";
            Item2hMeleeHolster2hUnholsterClips.AppendChild(Clip2hMeleeHolster2hUnholsterClips);
            Unholster2hMeleeUnholsterClips.AppendChild(Item2hMeleeHolsterMiniUnholsterClips);
            Item2hMeleeHolsterMiniUnholsterClips.AppendChild(Weapons2hMeleeHolsterMiniUnholsterClips);
            Clip2hMeleeHolsterMiniUnholsterClips.Value = "2h_melee_holster_mini";
            Item2hMeleeHolsterMiniUnholsterClips.AppendChild(Clip2hMeleeHolsterMiniUnholsterClips);
            //UNHOLSTER_1H
            MovementModeUnholsterData.AppendChild(ItemUnholster1h);
            NameUnholster1h.Value = "UNHOLSTER_1H";
            ItemUnholster1h.AppendChild(NameUnholster1h);
            ItemUnholster1h.AppendChild(Unholster1hUnholsterClips);
            Unholster1hUnholsterClips.AppendChild(Item1hHolsterUnarmedUnholsterClips);
            Item1hHolsterUnarmedUnholsterClips.AppendChild(Weapons1hHolsterUnarmedUnholsterClips);
            Clip1hHolsterUnarmedUnholsterClips.Value = "1h_holster_unarmed";
            Item1hHolsterUnarmedUnholsterClips.AppendChild(Clip1hHolsterUnarmedUnholsterClips);
            Unholster1hUnholsterClips.AppendChild(Item1hHolster2hMeleeUnholsterClips);
            Item1hHolster2hMeleeUnholsterClips.AppendChild(Weapons1hHolster2hMeleeUnholsterClips);
            Clip1hHolster2hMeleeUnholsterClips.Value = "1h_holster_2h_melee";
            Item1hHolster2hMeleeUnholsterClips.AppendChild(Clip1hHolster2hMeleeUnholsterClips);
            Unholster1hUnholsterClips.AppendChild(Item1hHolster1hUnholsterClips);
            Item1hHolster1hUnholsterClips.AppendChild(Weapons1hHolster1hUnholsterClips);
            Clip1hHolster1hUnholsterClips.Value = "1h_holster_1h";
            Item1hHolster1hUnholsterClips.AppendChild(Clip1hHolster1hUnholsterClips);
            Unholster1hUnholsterClips.AppendChild(Item1hHolster2hUnholsterClips);
            Item1hHolster2hUnholsterClips.AppendChild(Weapons1hHolster2hUnholsterClips);
            Clip1hHolster2hUnholsterClips.Value = "1h_holster_2h";
            Item1hHolster2hUnholsterClips.AppendChild(Clip1hHolster2hUnholsterClips);
            Unholster1hUnholsterClips.AppendChild(Item1hHolsterMiniUnholsterClips);
            Item1hHolsterMiniUnholsterClips.AppendChild(Weapons1hHolsterMiniUnholsterClips);
            Clip1hHolsterMiniUnholsterClips.Value = "1h_holster_mini";
            Item1hHolsterMiniUnholsterClips.AppendChild(Clip1hHolsterMiniUnholsterClips);
            //UNHOLSTER_2H
            MovementModeUnholsterData.AppendChild(ItemUnholster2h);
            NameUnholster2h.Value = "UNHOLSTER_2H";
            ItemUnholster2h.AppendChild(NameUnholster2h);
            ItemUnholster2h.AppendChild(Unholster2hUnholsterClips);
            Unholster2hUnholsterClips.AppendChild(Item2hHolsterUnarmedUnholsterClips);
            Item2hHolsterUnarmedUnholsterClips.AppendChild(Weapons2hHolsterUnarmedUnholsterClips);
            Clip2hHolsterUnarmedUnholsterClips.Value = "2h_holster_unarmed";
            Item2hHolsterUnarmedUnholsterClips.AppendChild(Clip2hHolsterUnarmedUnholsterClips);
            Unholster2hUnholsterClips.AppendChild(Item2hHolster2hMeleeUnholsterClips);
            Item2hHolster2hMeleeUnholsterClips.AppendChild(Weapons2hHolster2hMeleeUnholsterClips);
            Clip2hHolster2hMeleeUnholsterClips.Value = "2h_holster_2h_melee";
            Item2hHolster2hMeleeUnholsterClips.AppendChild(Clip2hHolster2hMeleeUnholsterClips);
            Unholster2hUnholsterClips.AppendChild(Item2hHolster1hUnholsterClips);
            Item2hHolster1hUnholsterClips.AppendChild(Weapons2hHolster1hUnholsterClips);
            Clip2hHolster1hUnholsterClips.Value = "2h_holster_1h";
            Item2hHolster1hUnholsterClips.AppendChild(Clip2hHolster1hUnholsterClips);
            Unholster2hUnholsterClips.AppendChild(Item2hHolster2hUnholsterClips);
            Item2hHolster2hUnholsterClips.AppendChild(Weapons2hHolster2hUnholsterClips);
            Clip2hHolster2hUnholsterClips.Value = "2h_holster_2h";
            Item2hHolster2hUnholsterClips.AppendChild(Clip2hHolster2hUnholsterClips);
            Unholster2hUnholsterClips.AppendChild(Item2hHolsterMiniUnholsterClips);
            Item2hHolsterMiniUnholsterClips.AppendChild(Weapons2hHolsterMiniUnholsterClips);
            Clip2hHolsterMiniUnholsterClips.Value = "2h_holster_mini";
            Item2hHolsterMiniUnholsterClips.AppendChild(Clip2hHolsterMiniUnholsterClips);
            //UNHOLSTER_MINIGUN
            MovementModeUnholsterData.AppendChild(ItemUnholsterMinigun);
            NameUnholsterMinigun.Value = "UNHOLSTER_MINIGUN";
            ItemUnholsterMinigun.AppendChild(NameUnholsterMinigun);
            ItemUnholsterMinigun.AppendChild(UnholsterMinigunUnholsterClips);
            UnholsterMinigunUnholsterClips.AppendChild(ItemMiniHolsterUnarmedUnholsterClips);
            ItemMiniHolsterUnarmedUnholsterClips.AppendChild(WeaponsMiniHolsterUnarmedUnholsterClips);
            ClipMiniHolsterUnarmedUnholsterClips.Value = "mini_holster_2h_unarmed";
            ItemMiniHolsterUnarmedUnholsterClips.AppendChild(ClipMiniHolsterUnarmedUnholsterClips);
            UnholsterMinigunUnholsterClips.AppendChild(ItemMiniHolster2hMeleeUnholsterClips);
            ItemMiniHolster2hMeleeUnholsterClips.AppendChild(WeaponsMiniHolster2hMeleeUnholsterClips);
            ClipMiniHolster2hMeleeUnholsterClips.Value = "mini_holster_2h_melee";
            ItemMiniHolster2hMeleeUnholsterClips.AppendChild(ClipMiniHolster2hMeleeUnholsterClips);
            UnholsterMinigunUnholsterClips.AppendChild(ItemMiniHolster1hUnholsterClips);
            ItemMiniHolster1hUnholsterClips.AppendChild(WeaponsMiniHolster1hUnholsterClips);
            ClipMiniHolster1hUnholsterClips.Value = "mini_holster_1h";
            ItemMiniHolster1hUnholsterClips.AppendChild(ClipMiniHolster1hUnholsterClips);
            UnholsterMinigunUnholsterClips.AppendChild(ItemMiniHolster2hUnholsterClips);
            ItemMiniHolster1hUnholsterClips.AppendChild(WeaponsMiniHolster2hUnholsterClips);
            ClipMiniHolster2hUnholsterClips.Value = "mini_holster_2h";
            ItemMiniHolster2hUnholsterClips.AppendChild(ClipMiniHolster2hUnholsterClips);
            //UNHOLSTER_UNARMED_STEALTH
            MovementModeUnholsterData.AppendChild(ItemUnholsterUnarmedStealth);
            NameUnholsterUnarmedStealth.Value = "UNHOLSTER_UNARMED_STEALTH";
            ItemUnholsterUnarmedStealth.AppendChild(NameUnholsterUnarmedStealth);
            ItemUnholsterUnarmedStealth.AppendChild(UnholsterUnarmedStealthUnholsterClips);
            UnholsterUnarmedStealthUnholsterClips.AppendChild(ItemUnarmedStealthHolsterUnarmedUnholsterClips);
            ItemUnarmedStealthHolsterUnarmedUnholsterClips.AppendChild(WeaponsUnarmedStealthHolsterUnarmedUnholsterClips);
            ClipUnarmedStealthHolsterUnarmedUnholsterClips.Value = "unarmed_holster_unarmed";
            ItemUnarmedStealthHolsterUnarmedUnholsterClips.AppendChild(ClipUnarmedStealthHolsterUnarmedUnholsterClips);
            UnholsterUnarmedStealthUnholsterClips.AppendChild(ItemUnarmedStealthHolster1hUnholsterClips);
            ItemUnarmedStealthHolster1hUnholsterClips.AppendChild(WeaponsUnarmedStealthHolster1hUnholsterClips);
            ClipUnarmedStealthHolster1hUnholsterClips.Value = "unarmed_holster_1h";
            ItemUnarmedStealthHolster1hUnholsterClips.AppendChild(ClipUnarmedStealthHolster1hUnholsterClips);
            UnholsterUnarmedStealthUnholsterClips.AppendChild(ItemUnarmedStealthHolster2hUnholsterClips);
            ItemUnarmedStealthHolster2hUnholsterClips.AppendChild(WeaponsUnarmedStealthHolster2hUnholsterClips);
            ClipUnarmedStealthHolster2hUnholsterClips.Value = "unarmed_holster_2h";
            ItemUnarmedStealthHolster2hUnholsterClips.AppendChild(ClipUnarmedStealthHolster2hUnholsterClips);
            UnholsterUnarmedStealthUnholsterClips.AppendChild(ItemUnarmedStealthHolsterMiniUnholsterClips);
            ItemUnarmedStealthHolsterMiniUnholsterClips.AppendChild(WeaponsUnarmedStealthHolsterMiniUnholsterClips);
            ClipUnarmedStealthHolsterMiniUnholsterClips.Value = "unarmed_holster_mini";
            ItemUnarmedStealthHolsterMiniUnholsterClips.AppendChild(ClipUnarmedStealthHolsterMiniUnholsterClips);
            //UNHOLSTER_2H_MELEE_STEALTH
            MovementModeUnholsterData.AppendChild(ItemUnholster2hMeleeStealth);
            NameUnholster2hMeleeStealth.Value = "UNHOLSTER_2H_MELEE_STEALTH";
            ItemUnholster2hMeleeStealth.AppendChild(NameUnholster2hMeleeStealth);
            ItemUnholster2hMeleeStealth.AppendChild(Unholster2hMeleeStealthUnholsterClips);
            Unholster2hMeleeStealthUnholsterClips.AppendChild(Item2hMeleeStealthHolsterUnarmedUnholsterClips);
            Item2hMeleeStealthHolsterUnarmedUnholsterClips.AppendChild(Weapons2hMeleeStealthHolsterUnarmedUnholsterClips);
            Clip2hMeleeStealthHolsterUnarmedUnholsterClips.Value = "unarmed_holster_unarmed";
            Item2hMeleeStealthHolsterUnarmedUnholsterClips.AppendChild(Clip2hMeleeStealthHolsterUnarmedUnholsterClips);
            Unholster2hMeleeStealthUnholsterClips.AppendChild(Item2hMeleeStealthHolster1hUnholsterClips);
            Item2hMeleeStealthHolster1hUnholsterClips.AppendChild(Weapons2hMeleeStealthHolster1hUnholsterClips);
            Clip2hMeleeStealthHolster1hUnholsterClips.Value = "unarmed_holster_1h";
            Item2hMeleeStealthHolster1hUnholsterClips.AppendChild(Clip2hMeleeStealthHolster1hUnholsterClips);
            Unholster2hMeleeStealthUnholsterClips.AppendChild(Item2hMeleeStealthHolster2hUnholsterClips);
            Item2hMeleeStealthHolster2hUnholsterClips.AppendChild(Weapons2hMeleeStealthHolster2hUnholsterClips);
            Clip2hMeleeStealthHolster2hUnholsterClips.Value = "unarmed_holster_2h";
            Item2hMeleeStealthHolster2hUnholsterClips.AppendChild(Clip2hMeleeStealthHolster2hUnholsterClips);
            //UNHOLSTER_1H_STEALTH
            MovementModeUnholsterData.AppendChild(ItemUnholster1hStealth);
            NameUnholster1hStealth.Value = "UNHOLSTER_1H_STEALTH";
            ItemUnholster1hStealth.AppendChild(NameUnholster1hStealth);
            ItemUnholster1hStealth.AppendChild(Unholster1hStealthUnholsterClips);
            Unholster1hStealthUnholsterClips.AppendChild(Item1hStealthHolsterUnarmedUnholsterClips);
            Item1hStealthHolsterUnarmedUnholsterClips.AppendChild(Weapons1hStealthHolsterUnarmedUnholsterClips);
            Clip1hStealthHolsterUnarmedUnholsterClips.Value = "1h_holster_unarmed";
            Item1hStealthHolsterUnarmedUnholsterClips.AppendChild(Clip1hStealthHolsterUnarmedUnholsterClips);
            Unholster1hStealthUnholsterClips.AppendChild(Item1hStealthHolster1hUnholsterClips);
            Item1hStealthHolster1hUnholsterClips.AppendChild(Weapons1hStealthHolster1hUnholsterClips);
            Clip1hStealthHolster1hUnholsterClips.Value = "1h_holster_1h";
            Item1hStealthHolster1hUnholsterClips.AppendChild(Clip1hStealthHolster1hUnholsterClips);
            Unholster1hStealthUnholsterClips.AppendChild(Item1hStealthHolster2hUnholsterClips);
            Item1hStealthHolster2hUnholsterClips.AppendChild(Weapons1hStealthHolster2hUnholsterClips);
            Clip1hStealthHolster2hUnholsterClips.Value = "1h_holster_2h";
            Item1hStealthHolster2hUnholsterClips.AppendChild(Clip1hStealthHolster2hUnholsterClips);
            //UNHOLSTER_2H_STEALTH
            MovementModeUnholsterData.AppendChild(ItemUnholster2hStealth);
            NameUnholster2hStealth.Value = "UNHOLSTER_2H_STEALTH";
            ItemUnholster2hStealth.AppendChild(NameUnholster2hStealth);
            ItemUnholster2hStealth.AppendChild(Unholster2hStealthUnholsterClips);
            Unholster2hStealthUnholsterClips.AppendChild(Item2hStealthHolsterUnarmedUnholsterClips);
            Item2hStealthHolsterUnarmedUnholsterClips.AppendChild(Weapons2hStealthHolsterUnarmedUnholsterClips);
            Clip2hStealthHolsterUnarmedUnholsterClips.Value = "2h_holster_unarmed";
            Item2hStealthHolsterUnarmedUnholsterClips.AppendChild(Clip2hStealthHolsterUnarmedUnholsterClips);
            Unholster2hStealthUnholsterClips.AppendChild(Item2hStealthHolster1hUnholsterClips);
            Item2hStealthHolster1hUnholsterClips.AppendChild(Weapons2hStealthHolster1hUnholsterClips);
            Clip2hStealthHolster1hUnholsterClips.Value = "2h_holster_1h";
            Item2hStealthHolster1hUnholsterClips.AppendChild(Clip2hStealthHolster1hUnholsterClips);
            Unholster2hStealthUnholsterClips.AppendChild(Item2hStealthHolster2hUnholsterClips);
            Item2hStealthHolster2hUnholsterClips.AppendChild(Weapons2hStealthHolster2hUnholsterClips);
            Clip2hStealthHolster2hUnholsterClips.Value = "2h_holster_2h";
            Item2hStealthHolster2hUnholsterClips.AppendChild(Clip2hStealthHolster2hUnholsterClips);
            //MovementModes
            CPedModelInfo__PersonalityDataList.AppendChild(MovementModes);
            //DEFAULT_ACTION
            MovementModes.AppendChild(ItemDefaultAction);
            NameDefaultAction.Value = "DEFAULT_ACTION";
            ItemDefaultAction.AppendChild(NameDefaultAction);
            ItemDefaultAction.AppendChild(DefaultActionMovementNodes);
            DefaultActionMovementNodes.AppendChild(ItemDefaultActionNormal);
            DefaultActionMovementNodes.AppendChild(ItemDefaultActionStealth);
            //MP_FEMALE_ACTION
            MovementModes.AppendChild(ItemMpFemaleAction);
            NameMpFemaleAction.Value = "MP_FEMALE_ACTION";
            ItemMpFemaleAction.AppendChild(NameMpFemaleAction);
            ItemMpFemaleAction.AppendChild(MpFemaleActionMovementNodes);
            MpFemaleActionMovementNodes.AppendChild(ItemMpFemaleActionNormal);
            MpFemaleActionMovementNodes.AppendChild(ItemMpFemaleActionStealth);
            //MICHAEL_ACTION
            MovementModes.AppendChild(ItemMichaelAction);
            NameMichaelAction.Value = "MICHAEL_ACTION";
            ItemMichaelAction.AppendChild(NameMichaelAction);
            ItemMichaelAction.AppendChild(MichaelActionMovementNodes);
            MichaelActionMovementNodes.AppendChild(ItemMichaelActionNormal);
            MichaelActionMovementNodes.AppendChild(ItemMichaelActionStealth);
            //FRANKLIN_ACTION
            MovementModes.AppendChild(ItemFranklinAction);
            NameFranklinAction.Value = "FRANKLIN_ACTION";
            ItemFranklinAction.AppendChild(NameFranklinAction);
            ItemFranklinAction.AppendChild(FranklinActionMovementNodes);
            FranklinActionMovementNodes.AppendChild(ItemFranklinActionNormal);
            FranklinActionMovementNodes.AppendChild(ItemFranklinActionStealth);
            //TREVOR_ACTION
            MovementModes.AppendChild(ItemTrevorAction);
            NameTrevorAction.Value = "TREVOR_ACTION";
            ItemTrevorAction.AppendChild(NameTrevorAction);
            ItemTrevorAction.AppendChild(TrevorActionMovementNodes);
            TrevorActionMovementNodes.AppendChild(ItemTrevorActionNormal);
            TrevorActionMovementNodes.AppendChild(ItemTrevorActionStealth);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED\"]/UnholsterClips/Item[Clip=\"unarmed_holster_unarmed\"]/Weapons/Item");
                
                //MovementModeUnholsterData
                //UNHOLSTER_UNARMED

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsUnarmedUnholsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsUnarmedUnholsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED\"]/UnholsterClips/Item[Clip=\"unarmed_holster_2h_melee\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeUnholsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeUnholsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED\"]/UnholsterClips/Item[Clip=\"unarmed_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hUnholsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hUnholsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED\"]/UnholsterClips/Item[Clip=\"unarmed_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hUnholsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hUnholsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED\"]/UnholsterClips/Item[Clip=\"unarmed_holster_mini\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsMiniUnholsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsMiniUnholsterUnarmedUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_2H_MELEE

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE\"]/UnholsterClips/Item[Clip=\"2h_melee_holster_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE\"]/UnholsterClips/Item[Clip=\"2h_melee_holster_2h_melee\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeHolster2hMeleeUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeHolster2hMeleeUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE\"]/UnholsterClips/Item[Clip=\"2h_melee_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE\"]/UnholsterClips/Item[Clip=\"2h_melee_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeHolster2hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE\"]/UnholsterClips/Item[Clip=\"2h_melee_holster_mini\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeHolsterMiniUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeHolsterMiniUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_1H

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H\"]/UnholsterClips/Item[Clip=\"1h_holster_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H\"]/UnholsterClips/Item[Clip=\"1h_holster_2h_melee\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hHolster2hMeleeUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hHolster2hMeleeUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H\"]/UnholsterClips/Item[Clip=\"1h_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H\"]/UnholsterClips/Item[Clip=\"1h_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hHolster2hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H\"]/UnholsterClips/Item[Clip=\"1h_holster_mini\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hHolsterMiniUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hHolsterMiniUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_2H

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H\"]/UnholsterClips/Item[Clip=\"2h_holster_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H\"]/UnholsterClips/Item[Clip=\"2h_holster_2h_melee\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hHolster2hMeleeUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hHolster2hMeleeUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H\"]/UnholsterClips/Item[Clip=\"2h_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H\"]/UnholsterClips/Item[Clip=\"2h_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hHolster2hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H\"]/UnholsterClips/Item[Clip=\"2h_holster_mini\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hHolsterMiniUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hHolsterMiniUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_MINIGUN

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_MINIGUN\"]/UnholsterClips/Item[Clip=\"mini_holster_2h_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsMiniHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsMiniHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_MINIGUN\"]/UnholsterClips/Item[Clip=\"mini_holster_2h_melee\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsMiniHolster2hMeleeUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsMiniHolster2hMeleeUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_MINIGUN\"]/UnholsterClips/Item[Clip=\"mini_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsMiniHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsMiniHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_MINIGUN\"]/UnholsterClips/Item[Clip=\"mini_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsMiniHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsMiniHolster2hUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_UNARMED_STEALTH

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED_STEALTH\"]/UnholsterClips/Item[Clip=\"unarmed_holster_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsUnarmedStealthHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsUnarmedStealthHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED_STEALTH\"]/UnholsterClips/Item[Clip=\"unarmed_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsUnarmedStealthHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsUnarmedStealthHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED_STEALTH\"]/UnholsterClips/Item[Clip=\"unarmed_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsUnarmedStealthHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsUnarmedStealthHolster2hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_UNARMED_STEALTH\"]/UnholsterClips/Item[Clip=\"unarmed_holster_mini\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponsUnarmedStealthHolsterMiniUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponsUnarmedStealthHolsterMiniUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_2H_MELEE_STEALTH

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE_STEALTH\"]/UnholsterClips/Item[Clip=\"unarmed_holster_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeStealthHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeStealthHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE_STEALTH\"]/UnholsterClips/Item[Clip=\"unarmed_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeStealthHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeStealthHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_MELEE_STEALTH\"]/UnholsterClips/Item[Clip=\"unarmed_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hMeleeStealthHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hMeleeStealthHolster2hUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_1H_STEALTH

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H_STEALTH\"]/UnholsterClips/Item[Clip=\"1h_holster_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hStealthHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hStealthHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H_STEALTH\"]/UnholsterClips/Item[Clip=\"1h_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hStealthHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hStealthHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_1H_STEALTH\"]/UnholsterClips/Item[Clip=\"1h_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons1hStealthHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons1hStealthHolster2hUnholsterClips.AppendChild(node);
                }

                //UNHOLSTER_2H_STEALTH

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_STEALTH\"]/UnholsterClips/Item[Clip=\"2h_holster_unarmed\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hStealthHolsterUnarmedUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hStealthHolsterUnarmedUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_STEALTH\"]/UnholsterClips/Item[Clip=\"2h_holster_1h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hStealthHolster1hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hStealthHolster1hUnholsterClips.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModeUnholsterData/Item[Name=\"UNHOLSTER_2H_STEALTH\"]/UnholsterClips/Item[Clip=\"2h_holster_2h\"]/Weapons/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Weapons2hStealthHolster2hUnholsterClips.OwnerDocument.ImportNode(nodes[i], true);
                    Weapons2hStealthHolster2hUnholsterClips.AppendChild(node);
                }

                //MovementModes
                //DEFAULT_ACTION

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModes/Item[Name=\"DEFAULT_ACTION\"]/MovementModes/Item");
                XmlNodeList movementItems;

                for (int i = 0; i < nodes.Count; i++)
                {
                    movementItems = nodes[i].SelectNodes("Item");

                    for (int j = 0; j < movementItems.Count; j++)
                    {
                        if (i == 0)
                        {
                            XmlNode node = ItemDefaultActionNormal.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemDefaultActionNormal.AppendChild(node);

                        }
                        else
                        {
                            XmlNode node = ItemDefaultActionStealth.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemDefaultActionStealth.AppendChild(node);
                        }
                    }
                }

                //MP_FEMALE_ACTION

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModes/Item[Name=\"MP_FEMALE_ACTION\"]/MovementModes/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    movementItems = nodes[i].SelectNodes("Item");

                    for (int j = 0; j < movementItems.Count; j++)
                    {
                        if (i == 0)
                        {
                            XmlNode node = ItemMpFemaleActionNormal.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemMpFemaleActionNormal.AppendChild(node);

                        }
                        else
                        {
                            XmlNode node = ItemMpFemaleActionStealth.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemMpFemaleActionStealth.AppendChild(node);
                        }
                    }
                }

                //MICHAEL_ACTION

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModes/Item[Name=\"MICHAEL_ACTION\"]/MovementModes/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    movementItems = nodes[i].SelectNodes("Item");

                    for (int j = 0; j < movementItems.Count; j++)
                    {
                        if (i == 0)
                        {
                            XmlNode node = ItemMichaelActionNormal.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemMichaelActionNormal.AppendChild(node);

                        }
                        else
                        {
                            XmlNode node = ItemMichaelActionStealth.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemMichaelActionStealth.AppendChild(node);
                        }
                    }
                }

                //FRANKLIN_ACTION

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModes/Item[Name=\"FRANKLIN_ACTION\"]/MovementModes/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    movementItems = nodes[i].SelectNodes("Item");

                    for (int j = 0; j < movementItems.Count; j++)
                    {
                        if (i == 0)
                        {
                            XmlNode node = ItemFranklinActionNormal.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemFranklinActionNormal.AppendChild(node);

                        }
                        else
                        {
                            XmlNode node = ItemFranklinActionStealth.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemFranklinActionStealth.AppendChild(node);
                        }
                    }
                }

                //TREVOR_ACTION

                nodes = xmlDocument.SelectNodes("/CPedModelInfo__PersonalityDataList/MovementModes/Item[Name=\"TREVOR_ACTION\"]/MovementModes/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    movementItems = nodes[i].SelectNodes("Item");

                    for (int j = 0; j < movementItems.Count; j++)
                    {
                        if (i == 0)
                        {
                            XmlNode node = ItemTrevorActionNormal.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemTrevorActionNormal.AppendChild(node);

                        }
                        else
                        {
                            XmlNode node = ItemTrevorActionStealth.OwnerDocument.ImportNode(movementItems[j], true);
                            ItemTrevorActionStealth.AppendChild(node);
                        }
                    }
                }
            }
            CPedModelInfo__PersonalityDataList.WriteTo(writer);
            writer.Close();
        }

        private static void MergeWeaponComponents()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CWeaponComponentInfoBlob = nuevo.CreateElement("CWeaponComponentInfoBlob");
            XmlNode Infos = nuevo.CreateElement("Infos");
            XmlWriter writer = XmlWriter.Create("output/weapons/weaponcomponents.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/weaponcomponents.meta");
            String ruta = PedirRuta();

            CWeaponComponentInfoBlob.AppendChild(Infos);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CWeaponComponentInfoBlob/Infos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Infos.OwnerDocument.ImportNode(nodes[i], true);
                    Infos.AppendChild(node);
                }
            }
            CWeaponComponentInfoBlob.WriteTo(writer);
            writer.Close();
        }

        private static void MergeWeaponArchetypes()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode InitDataList = nuevo.CreateElement("CWeaponModelInfo__InitDataList");
            XmlNode InitDatas = nuevo.CreateElement("InitDatas");
            XmlWriter writer = XmlWriter.Create("output/weapons/weaponarchetypes.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/weaponarchetypes.meta");
            String ruta = PedirRuta();

            InitDataList.AppendChild(InitDatas);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CWeaponModelInfo__InitDataList/InitDatas/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = InitDatas.OwnerDocument.ImportNode(nodes[i], true);
                    InitDatas.AppendChild(node);
                }
            }
            InitDataList.WriteTo(writer);
            writer.Close();
        }

        private static void MergeWeaponAnimations()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CWeaponAnimationsSets = nuevo.CreateElement("CWeaponAnimationsSets");
            XmlNode WeaponAnimationsSets = nuevo.CreateElement("WeaponAnimationsSets");
            XmlElement ItemBallistic = nuevo.CreateElement("Item");
            XmlElement Fallback = nuevo.CreateElement("Fallback");
            XmlElement WeaponAnimationsBallistic = nuevo.CreateElement("WeaponAnimations");
            XmlElement ItemDefault = nuevo.CreateElement("Item");
            XmlElement WeaponAnimationsDefault = nuevo.CreateElement("WeaponAnimations");
            XmlElement ItemGang = nuevo.CreateElement("Item");
            XmlElement WeaponAnimationsGang = nuevo.CreateElement("WeaponAnimations");
            XmlElement ItemFirstPerson = nuevo.CreateElement("Item");
            XmlElement WeaponAnimationsFirstPerson = nuevo.CreateElement("WeaponAnimations");
            XmlElement ItemFirstPersonAiming = nuevo.CreateElement("Item");
            XmlElement WeaponAnimationsFirstPersonAiming = nuevo.CreateElement("WeaponAnimations");
            XmlElement ItemFirstPersonRNG = nuevo.CreateElement("Item");
            XmlElement WeaponAnimationsFirstPersonRNG = nuevo.CreateElement("WeaponAnimations");
            XmlElement ItemFirstPersonScope = nuevo.CreateElement("Item");
            XmlElement WeaponAnimationsFirstPersonScope = nuevo.CreateElement("WeaponAnimations");
            XmlWriter writer = XmlWriter.Create("output/weapons/weaponanimations.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/weaponanimations.meta");
            String ruta = PedirRuta();
            CWeaponAnimationsSets.AppendChild(WeaponAnimationsSets);
            ItemBallistic.SetAttribute("key", "Ballistic");
            WeaponAnimationsSets.AppendChild(ItemBallistic);
            Fallback.Value = "Default";
            ItemBallistic.AppendChild(Fallback);
            ItemBallistic.AppendChild(WeaponAnimationsBallistic);
            ItemDefault.SetAttribute("key", "Default");
            WeaponAnimationsSets.AppendChild(ItemDefault);
            ItemDefault.AppendChild(WeaponAnimationsDefault);
            ItemGang.SetAttribute("key", "Gang");
            WeaponAnimationsSets.AppendChild(ItemGang);
            ItemGang.AppendChild(Fallback);
            ItemGang.AppendChild(WeaponAnimationsGang);
            ItemFirstPerson.SetAttribute("key", "FirstPerson");
            WeaponAnimationsSets.AppendChild(ItemFirstPerson);
            ItemFirstPerson.AppendChild(Fallback);
            ItemFirstPerson.AppendChild(WeaponAnimationsFirstPerson);
            ItemFirstPersonAiming.SetAttribute("key", "FirstPersonAiming");
            WeaponAnimationsSets.AppendChild(ItemFirstPersonAiming);
            ItemFirstPersonAiming.AppendChild(Fallback);
            ItemFirstPersonAiming.AppendChild(WeaponAnimationsFirstPersonAiming);
            ItemFirstPersonRNG.SetAttribute("key", "FirstPersonRNG");
            WeaponAnimationsSets.AppendChild(ItemFirstPersonRNG);
            ItemFirstPersonRNG.AppendChild(Fallback);
            ItemFirstPersonRNG.AppendChild(WeaponAnimationsFirstPersonRNG);
            ItemFirstPersonScope.SetAttribute("key", "FirstPersonScope");
            WeaponAnimationsSets.AppendChild(ItemFirstPersonScope);
            ItemFirstPersonScope.AppendChild(Fallback);
            ItemFirstPersonScope.AppendChild(WeaponAnimationsFirstPersonScope);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CWeaponAnimationsSets/WeaponAnimationsSets/Item[key=\"Ballistic\"]/WeaponAnimations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponAnimationsBallistic.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponAnimationsBallistic.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponAnimationsSets/WeaponAnimationsSets/Item[key=\"Default\"]/WeaponAnimations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponAnimationsDefault.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponAnimationsDefault.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponAnimationsSets/WeaponAnimationsSets/Item[key=\"Gang\"]/WeaponAnimations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponAnimationsGang.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponAnimationsGang.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponAnimationsSets/WeaponAnimationsSets/Item[key=\"FirstPerson\"]/WeaponAnimations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponAnimationsFirstPerson.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponAnimationsFirstPerson.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponAnimationsSets/WeaponAnimationsSets/Item[key=\"FirstPersonAiming\"]/WeaponAnimations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponAnimationsFirstPersonAiming.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponAnimationsFirstPersonAiming.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponAnimationsSets/WeaponAnimationsSets/Item[key=\"FirstPersonRNG\"]/WeaponAnimations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponAnimationsFirstPersonRNG.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponAnimationsFirstPersonRNG.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponAnimationsSets/WeaponAnimationsSets/Item[key=\"FirstPersonScope\"]/WeaponAnimations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponAnimationsFirstPersonScope.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponAnimationsFirstPersonScope.AppendChild(node);
                }
            }
            CWeaponAnimationsSets.WriteTo(writer);
            writer.Close();
        }

        private static void MergeWeapons()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CWeaponInfoBlob = nuevo.CreateElement("CWeaponInfoBlob");
            XmlNode SlotNavigateOrder = nuevo.CreateElement("SlotNavigateOrder");
            XmlNode SlotBestOrder = nuevo.CreateElement("SlotBestOrder");
            XmlNode TintSpecValues = nuevo.CreateElement("TintSpecValues");
            XmlNode FiringPatternAliases = nuevo.CreateElement("FiringPatternAliases");
            XmlNode UpperBodyFixupExpressionData = nuevo.CreateElement("UpperBodyFixupExpressionData");
            XmlNode AimingInfos = nuevo.CreateElement("AimingInfos");
            XmlNode Infos = nuevo.CreateElement("Infos");
            XmlNode ItemInfosAmmo = nuevo.CreateElement("Item");
            XmlNode InfosItemInfosAmmo = nuevo.CreateElement("Infos");
            XmlNode ItemInfosWeapons = nuevo.CreateElement("Item");
            XmlNode InfosItemInfosWeapons = nuevo.CreateElement("Infos");
            XmlNode ItemInfosVehicleWeapons = nuevo.CreateElement("Item");
            XmlNode InfosItemInfosVehicleWeapons = nuevo.CreateElement("Infos");
            XmlNode ItemInfosEnviromental = nuevo.CreateElement("Item");
            XmlNode InfosItemInfosEnviromental = nuevo.CreateElement("Infos");
            XmlNode VehicleWeaponInfos = nuevo.CreateElement("VehicleWeaponInfos");
            XmlNode WeaponGroupDamageForArmouredVehicleGlass = nuevo.CreateElement("WeaponGroupDamageForArmouredVehicleGlass");
            XmlNode Name = nuevo.CreateElement("Name");
            XmlWriter writer = XmlWriter.Create("output/weapons/weapons.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/weapons.meta").AddInclude("**/weapon_*.meta");
            String ruta = PedirRuta();

            CWeaponInfoBlob.AppendChild(SlotNavigateOrder);
            CWeaponInfoBlob.AppendChild(SlotBestOrder);
            CWeaponInfoBlob.AppendChild(TintSpecValues);
            CWeaponInfoBlob.AppendChild(FiringPatternAliases);
            CWeaponInfoBlob.AppendChild(UpperBodyFixupExpressionData);
            CWeaponInfoBlob.AppendChild(AimingInfos);
            CWeaponInfoBlob.AppendChild(Infos);
            Infos.AppendChild(ItemInfosAmmo);
            ItemInfosAmmo.AppendChild(InfosItemInfosAmmo);
            Infos.AppendChild(ItemInfosWeapons);
            ItemInfosWeapons.AppendChild(InfosItemInfosWeapons);
            Infos.AppendChild(ItemInfosVehicleWeapons);
            ItemInfosVehicleWeapons.AppendChild(InfosItemInfosVehicleWeapons);
            Infos.AppendChild(ItemInfosEnviromental);
            ItemInfosEnviromental.AppendChild(InfosItemInfosEnviromental);
            CWeaponInfoBlob.AppendChild(VehicleWeaponInfos);
            CWeaponInfoBlob.AppendChild(WeaponGroupDamageForArmouredVehicleGlass);
            CWeaponInfoBlob.AppendChild(Name);

            Name.Value = Guid.NewGuid().ToString();

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/SlotNavigateOrder/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = SlotNavigateOrder.OwnerDocument.ImportNode(nodes[i], true);
                    SlotNavigateOrder.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/SlotBestOrder/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = SlotBestOrder.OwnerDocument.ImportNode(nodes[i], true);
                    SlotBestOrder.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/TintSpecValues/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = TintSpecValues.OwnerDocument.ImportNode(nodes[i], true);
                    TintSpecValues.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/FiringPatternAliases/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = FiringPatternAliases.OwnerDocument.ImportNode(nodes[i], true);
                    FiringPatternAliases.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/UpperBodyFixupExpressionData/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = UpperBodyFixupExpressionData.OwnerDocument.ImportNode(nodes[i], true);
                    UpperBodyFixupExpressionData.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/AimingInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = AimingInfos.OwnerDocument.ImportNode(nodes[i], true);
                    AimingInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/Infos/Item");

                XmlNodeList infoNodes = nodes[0].SelectNodes("/infos/item");

                for (int i = 0; i < infoNodes.Count; i++)
                {
                    XmlNode node = InfosItemInfosAmmo.OwnerDocument.ImportNode(infoNodes[i], true);
                    InfosItemInfosAmmo.AppendChild(node);
                }

                infoNodes = nodes[1].SelectNodes("/infos/item");

                for (int i = 0; i < infoNodes.Count; i++)
                {
                    XmlNode node = InfosItemInfosWeapons.OwnerDocument.ImportNode(infoNodes[i], true);
                    InfosItemInfosWeapons.AppendChild(node);
                }

                infoNodes = nodes[2].SelectNodes("/infos/item");

                for (int i = 0; i < infoNodes.Count; i++)
                {
                    XmlNode node = InfosItemInfosVehicleWeapons.OwnerDocument.ImportNode(infoNodes[i], true);
                    InfosItemInfosVehicleWeapons.AppendChild(node);
                }

                infoNodes = nodes[3].SelectNodes("/infos/item");

                for (int i = 0; i < infoNodes.Count; i++)
                {
                    XmlNode node = InfosItemInfosEnviromental.OwnerDocument.ImportNode(infoNodes[i], true);
                    InfosItemInfosEnviromental.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/VehicleWeaponInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleWeaponInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleWeaponInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CWeaponInfoBlob/WeaponGroupDamageForArmouredVehicleGlass/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = WeaponGroupDamageForArmouredVehicleGlass.OwnerDocument.ImportNode(nodes[i], true);
                    WeaponGroupDamageForArmouredVehicleGlass.AppendChild(node);
                }
            }
            CWeaponInfoBlob.WriteTo(writer);
            writer.Close();
        }

        private static void MergePedStream()
        {
            if (!Directory.Exists("output/peds/stream"))
            {
                Directory.CreateDirectory("output/peds/stream");
            }

            String ruta = PedirRuta();

            foreach (String path in Directory.GetDirectories(ruta))
            {
                foreach (FileInfo file in new DirectoryInfo(Path.Combine(path, "stream")).EnumerateFiles())
                {
                    file.CopyTo(Path.Combine("output/peds/stream", file.Name), true);
                }
            }
        }

        private static void MergeVehicleStream()
        {
            if (!Directory.Exists("output/vehicles/stream"))
            {
                Directory.CreateDirectory("output/vehicles/stream");
            }

            String ruta = PedirRuta();

            foreach (String path in Directory.GetDirectories(ruta))
            {
                foreach (FileInfo file in new DirectoryInfo(Path.Combine(path, "stream")).EnumerateFiles())
                {
                    file.CopyTo(Path.Combine("output/vehicles/stream", file.Name), true);
                }
            }
        }

        private static void ExtractVehicleNames()
        {
            if (!File.Exists("output/vehicles/vehicles.meta"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("output/vehicles/vehicles.meta");
                XmlNodeList nodes = xmlDocument.SelectNodes("/CVehicleModelInfo__InitDataList/InitDatas/Item");
                StreamWriter salida = File.CreateText("output/vehicles/VehicleNames.txt");

                for (int i = 0; i < nodes.Count; i++)
                {
                    salida.WriteLine(nodes[i].SelectSingleNode("modelName").InnerText);
                }
                salida.Flush();
                salida.Close();
            }
        }

        private static void MergeVehicleLayouts()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CVehicleMetadataMgr = nuevo.CreateElement("CVehicleMetadataMgr");
            XmlNode AnimRateSets = nuevo.CreateElement("AnimRateSets");
            XmlNode ClipSetMaps = nuevo.CreateElement("ClipSetMaps");
            XmlNode VehicleCoverBoundOffsetInfos = nuevo.CreateElement("VehicleCoverBoundOffsetInfos");
            XmlNode BicycleInfos = nuevo.CreateElement("BicycleInfos");
            XmlNode POVTuningInfos = nuevo.CreateElement("POVTuningInfos");
            XmlNode EntryAnimVariations = nuevo.CreateElement("EntryAnimVariations");
            XmlNode VehicleExtraPointsInfos = nuevo.CreateElement("VehicleExtraPointsInfos");
            XmlNode DrivebyWeaponGroups = nuevo.CreateElement("DrivebyWeaponGroups");
            XmlNode VehicleDriveByAnimInfos = nuevo.CreateElement("VehicleDriveByAnimInfos");
            XmlNode VehicleDriveByInfos = nuevo.CreateElement("VehicleDriveByInfos");
            XmlNode VehicleSeatInfos = nuevo.CreateElement("VehicleSeatInfos");
            XmlNode VehicleSeatAnimInfos = nuevo.CreateElement("VehicleSeatAnimInfos");
            XmlNode VehicleEntryPointInfos = nuevo.CreateElement("VehicleEntryPointInfos");
            XmlNode VehicleEntryPointAnimInfos = nuevo.CreateElement("VehicleEntryPointAnimInfos");
            XmlNode VehicleExplosionInfos = nuevo.CreateElement("VehicleExplosionInfos");
            XmlNode VehicleLayoutInfos = nuevo.CreateElement("VehicleLayoutInfos");
            XmlNode VehicleScenarioLayoutInfos = nuevo.CreateElement("VehicleScenarioLayoutInfos");
            XmlNode SeatOverrideAnimInfos = nuevo.CreateElement("SeatOverrideAnimInfos");
            XmlNode InVehicleOverrideInfos = nuevo.CreateElement("InVehicleOverrideInfos");
            XmlNode FirstPersonDriveByLookAroundData = nuevo.CreateElement("FirstPersonDriveByLookAroundData");
            XmlWriter writer = XmlWriter.Create("output/vehicles/vehiclelayouts.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/vehiclelayouts.meta");
            String ruta = PedirRuta();

            CVehicleMetadataMgr.AppendChild(AnimRateSets);
            CVehicleMetadataMgr.AppendChild(ClipSetMaps);
            CVehicleMetadataMgr.AppendChild(VehicleCoverBoundOffsetInfos);
            CVehicleMetadataMgr.AppendChild(BicycleInfos);
            CVehicleMetadataMgr.AppendChild(POVTuningInfos);
            CVehicleMetadataMgr.AppendChild(EntryAnimVariations);
            CVehicleMetadataMgr.AppendChild(VehicleExtraPointsInfos);
            CVehicleMetadataMgr.AppendChild(DrivebyWeaponGroups);
            CVehicleMetadataMgr.AppendChild(VehicleDriveByAnimInfos);
            CVehicleMetadataMgr.AppendChild(VehicleDriveByInfos);
            CVehicleMetadataMgr.AppendChild(VehicleSeatInfos);
            CVehicleMetadataMgr.AppendChild(VehicleSeatAnimInfos);
            CVehicleMetadataMgr.AppendChild(VehicleEntryPointInfos);
            CVehicleMetadataMgr.AppendChild(VehicleEntryPointAnimInfos);
            CVehicleMetadataMgr.AppendChild(VehicleExplosionInfos);
            CVehicleMetadataMgr.AppendChild(VehicleLayoutInfos);
            CVehicleMetadataMgr.AppendChild(VehicleScenarioLayoutInfos);
            CVehicleMetadataMgr.AppendChild(SeatOverrideAnimInfos);
            CVehicleMetadataMgr.AppendChild(InVehicleOverrideInfos);
            CVehicleMetadataMgr.AppendChild(FirstPersonDriveByLookAroundData);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/AnimRateSets/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = AnimRateSets.OwnerDocument.ImportNode(nodes[i], true);
                    AnimRateSets.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/ClipSetMaps/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = ClipSetMaps.OwnerDocument.ImportNode(nodes[i], true);
                    ClipSetMaps.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleCoverBoundOffsetInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleCoverBoundOffsetInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleCoverBoundOffsetInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/BicycleInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = BicycleInfos.OwnerDocument.ImportNode(nodes[i], true);
                    BicycleInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/POVTuningInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = POVTuningInfos.OwnerDocument.ImportNode(nodes[i], true);
                    POVTuningInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/EntryAnimVariations/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = EntryAnimVariations.OwnerDocument.ImportNode(nodes[i], true);
                    EntryAnimVariations.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleExtraPointsInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleExtraPointsInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleExtraPointsInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/DrivebyWeaponGroups/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = DrivebyWeaponGroups.OwnerDocument.ImportNode(nodes[i], true);
                    DrivebyWeaponGroups.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleDriveByAnimInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleDriveByAnimInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleDriveByAnimInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleDriveByInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleDriveByInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleDriveByInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleSeatInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleSeatInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleSeatInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleSeatAnimInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleSeatAnimInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleSeatAnimInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleEntryPointInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleEntryPointInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleEntryPointInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleEntryPointAnimInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleEntryPointAnimInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleEntryPointAnimInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleExplosionInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleExplosionInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleExplosionInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleLayoutInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleLayoutInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleLayoutInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/VehicleScenarioLayoutInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = VehicleScenarioLayoutInfos.OwnerDocument.ImportNode(nodes[i], true);
                    VehicleScenarioLayoutInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/SeatOverrideAnimInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = SeatOverrideAnimInfos.OwnerDocument.ImportNode(nodes[i], true);
                    SeatOverrideAnimInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/InVehicleOverrideInfos/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = InVehicleOverrideInfos.OwnerDocument.ImportNode(nodes[i], true);
                    InVehicleOverrideInfos.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleMetadataMgr/FirstPersonDriveByLookAroundData/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = FirstPersonDriveByLookAroundData.OwnerDocument.ImportNode(nodes[i], true);
                    FirstPersonDriveByLookAroundData.AppendChild(node);
                }
            }
            CVehicleMetadataMgr.WriteTo(writer);
            writer.Close();
        }

        private static void MergeHandling()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CHandlingDataMgr = nuevo.CreateElement("CHandlingDataMgr");
            XmlNode HandlingData = nuevo.CreateElement("HandlingData");
            XmlWriter writer = XmlWriter.Create("output/vehicles/handling.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/handling.meta");
            String ruta = PedirRuta();

            CHandlingDataMgr.AppendChild(HandlingData);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CHandlingDataMgr/HandlingData/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = HandlingData.OwnerDocument.ImportNode(nodes[i], true);
                    HandlingData.AppendChild(node);
                }
            }
            CHandlingDataMgr.WriteTo(writer);
            writer.Close();
        }

        private static void MergeCarvariations()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CVehicleModelInfoVariation = nuevo.CreateElement("CVehicleModelInfoVariation");
            XmlNode variationData = nuevo.CreateElement("variationData");
            XmlWriter writer = XmlWriter.Create("output/vehicles/carvariations.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/carvariations.meta");
            String ruta = PedirRuta();

            CVehicleModelInfoVariation.AppendChild(variationData);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CVehicleModelInfoVariation/variationData/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = variationData.OwnerDocument.ImportNode(nodes[i], true);
                    variationData.AppendChild(node);
                }
            }
            CVehicleModelInfoVariation.WriteTo(writer);
            writer.Close();
        }

        private static void MergeCarcols()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode CVehicleModelInfoVarGlobal = nuevo.CreateElement("CVehicleModelInfoVarGlobal");
            XmlNode Kits = nuevo.CreateElement("Kits");
            XmlNode Lights = nuevo.CreateElement("Lights");
            XmlNode Sirens = nuevo.CreateElement("Sirens");
            XmlWriter writer = XmlWriter.Create("output/vehicles/carcols.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/carcols.meta");
            String ruta = PedirRuta();

            CVehicleModelInfoVarGlobal.AppendChild(Kits);
            CVehicleModelInfoVarGlobal.AppendChild(Lights);
            CVehicleModelInfoVarGlobal.AppendChild(Sirens);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CVehicleModelInfoVarGlobal/Kits/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Kits.OwnerDocument.ImportNode(nodes[i], true);
                    Kits.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleModelInfoVarGlobal/Lights/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Lights.OwnerDocument.ImportNode(nodes[i], true);
                    Lights.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleModelInfoVarGlobal/Sirens/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = Sirens.OwnerDocument.ImportNode(nodes[i], true);
                    Sirens.AppendChild(node);
                }
            }
            CVehicleModelInfoVarGlobal.WriteTo(writer);
            writer.Close();
        }

        private static void ExtractPedNames()
        {
            if (!File.Exists("output/peds/peds.meta"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("output/peds/peds.meta");
                XmlNodeList nodes = xmlDocument.SelectNodes("/CPedModelInfo__InitDataList/InitDatas/Item");
                StreamWriter salida = File.CreateText("output/peds/PedsNames.txt");

                for (int i = 0; i < nodes.Count; i++)
                {
                    salida.WriteLine(nodes[i].SelectSingleNode("Name").InnerText);
                }
                salida.Flush();
                salida.Close();
            }
        }

        private static void MergeVehicles()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode InitDataList = nuevo.CreateElement("CVehicleModelInfo__InitDataList");
            XmlNode InitDatas = nuevo.CreateElement("InitDatas");
            XmlNode txdRelationships = nuevo.CreateElement("txdRelationships");
            XmlWriter writer = XmlWriter.Create("output/vehicles/vehicles.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/vehicles.meta");
            String ruta = PedirRuta();

            InitDataList.AppendChild(InitDatas);
            InitDataList.AppendChild(txdRelationships);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CVehicleModelInfo__InitDataList/InitDatas/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = InitDatas.OwnerDocument.ImportNode(nodes[i], true);
                    InitDatas.AppendChild(node);
                }

                nodes = xmlDocument.SelectNodes("/CVehicleModelInfo__InitDataList/txdRelationships/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = txdRelationships.OwnerDocument.ImportNode(nodes[i], true);
                    txdRelationships.AppendChild(node);
                }
            }
            InitDataList.WriteTo(writer);
            writer.Close();
        }

        private static void MergePeds()
        {
            XmlDocument nuevo = new XmlDocument();
            XmlNode InitDataList = nuevo.CreateElement("CPedModelInfo__InitDataList");
            XmlNode InitDatas = nuevo.CreateElement("InitDatas");
            XmlWriter writer = XmlWriter.Create("output/peds/peds.meta", settings);
            Matcher matcher = new Matcher().AddInclude("**/peds.meta");
            String ruta = PedirRuta();

            InitDataList.AppendChild(InitDatas);

            foreach (string file in matcher.GetResultsInFullPath(ruta))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                XmlNodeList nodes = xmlDocument.SelectNodes("/CPedModelInfo__InitDataList/InitDatas/Item");

                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlNode node = InitDatas.OwnerDocument.ImportNode(nodes[i], true);
                    InitDatas.AppendChild(node);
                }
            }
            InitDataList.WriteTo(writer);
            writer.Close();
        }

        private static string PedirRuta()
        {
            Boolean ok = true;
            String ruta;

            do
            {
                Console.WriteLine("Dame la ruta de las carpetas de las peds");
                ruta = Console.ReadLine();

                if (ruta == null || ruta == "")
                {
                    ok = false;
                }
                else if (Directory.Exists(ruta))
                {
                    ok = false;
                }
            } while (!ok);

            return ruta;
        }
    }
}