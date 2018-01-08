using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POSoop
{
    class POS
    {
        public Dictionary<int, Item> MyCard = new Dictionary<int, Item>();
        public Dictionary<int, int> Quantity = new Dictionary<int, int>();
        public Dictionary<int, int> Buy = new Dictionary<int, int>();
        
        public List<Item> ItemList;
         
        public void AllItem()
        {
            Console.WriteLine("***************** WELCOME TO Online Bazar Developed BY TANVIR *****************\n");
            ItemList = new List<Item>()
            {
                new Item() {Id=1,  ItemName = "Pen", ItemPrice=5, },
                new Item() {Id=2,  ItemName ="shirt", ItemPrice=100,},
                new Item() {Id=3,  ItemName = "Pant", ItemPrice=50,}
            };
            

            
        }
        public void AllQuantity()
        {
            Quantity.Add(1, 33);
            Quantity.Add(2, 14);
            Quantity.Add(3, 50);   
        }

        public void Buying()
        {

            Buy.Add(1, 0);
            Buy.Add(2, 0);
            Buy.Add(3, 0);

        }

        public void UserCheck()
        {
            Console.Write("For Admin press 0, For customer press 1: ");
            var UserInput = Convert.ToInt32(Console.ReadLine());
            if (UserInput == 0)
            {
                Admin();
            }
            else if (UserInput == 1)
            {
                Customer();

            }
            else
            {
                Console.WriteLine("Wrong Input.TryAgain");
                UserCheck();
            }
        }

        private void Admin()
        {
            Console.Write("Select 0 for exit, 1 to add new item, 2 to add stock to existing item: ");
            var adminInput = Convert.ToInt32(Console.ReadLine());
            if (adminInput == 0)
            {
                UserCheck();
            }
            else if (adminInput == 1)
            {
                AddItem();
            }
            else if (adminInput == 2)
            {
                AddStock();
            }
            else
            {
                Console.WriteLine("Wrong Input.TryAgain");
                Admin();
            }
        }

        private void ProductShow()
        {
            Console.WriteLine("Serial\tItemName\tItemPrice\tQuantity");
            int i = 0;
            foreach (Item myItem in ItemList)
            {

                Console.WriteLine(++i + "\t" + myItem.ItemName + "\t\t" + myItem.ItemPrice + "\t\t" + Quantity[myItem.Id]);

            }

            Console.ReadLine();
        }

        private void Customer()
        {
            ProductShow();
            Console.WriteLine("Press 0 to Buy and 1 to Exit ");
            var CustomerChoice = Convert.ToInt32(Console.ReadLine());
            if (CustomerChoice==0)
            {
                CustomerBuy();
            }
            else if (CustomerChoice == 1)
            {
                UserCheck();
            }
            else
            {
                Console.WriteLine("Wrong Choice.Try Again ");
                UserCheck();
            }
        }

        private void AddItem()
        {
            Console.Write("Enter Item Name: ");
            var itemNameInput = Console.ReadLine();
            Console.Write("Enter Item Quantity: ");
            var itemQuantityInput = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Item Price: ");
            var itemPriceInput = Convert.ToInt32(Console.ReadLine());
            int j = ItemList.Count;
            ItemList.Add(new Item() {Id =++j, ItemName = itemNameInput, ItemPrice = itemPriceInput});
            Quantity.Add(j++,itemQuantityInput);
            Console.Write("Enter 0 to Add More or 1 to Check Products: ");
            var AddCheck = Convert.ToInt32(Console.ReadLine());
            if (AddCheck == 0)
            {
                AddItem();
            }
            else
            {
                ProductShow();   
            }
            
        }

        private void AddStock()
        {

          
            ProductShow();
            Console.Write("What You Want to Add? Enter Serial: ");
            var serialInput = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Item Quantity: ");
            var itemNumberAdd = Convert.ToInt32(Console.ReadLine());
            Quantity[serialInput] += itemNumberAdd;

            Console.Write("Enter 0 to Add More Quantity or 1 to Check All Quantity: ");
            var AddQuantity = Convert.ToInt32(Console.ReadLine());
            if (AddQuantity == 0)
            {
                AddStock();
            }
            else
            {
                ProductShow();
            }
            


        }

        
        public void CustomerBuy()
        {
            Console.Write("Enter Serial Number to Buy the Product: ");
            var SerialChoice = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Quantity of the Product: ");
            var SerialQuantity = Convert.ToInt32(Console.ReadLine());
            if (!MyCard.ContainsKey(SerialChoice) && Quantity[SerialChoice] >= SerialQuantity)
            {




                Quantity[SerialChoice] -= SerialQuantity; 
                MyCard.Add(SerialChoice, ItemList[SerialChoice - 1]);
                //Buy.Add(SerialChoice,SerialQuantity); 
                Buy[SerialChoice] += SerialQuantity;

            }

            else if (Quantity[SerialChoice] >= SerialQuantity )
            {
                Quantity[SerialChoice] -= SerialQuantity;

                Buy[SerialChoice] +=SerialQuantity;
               
               
            }
            else
            {
                Console.WriteLine("Sorry Quantity is Insufficient. Try Again");
                CustomerBuy();
            }
            MoreBuy();

        }

        public void MoreBuy()
        {
            var TotalPrice = 0;
            Console.Write("Press 0 to Buy More or 1 to check Card: ");
            var BuyInput = Convert.ToInt32(Console.ReadLine());
            if (BuyInput==0)
            {
                CustomerBuy();
            }
            else if (BuyInput == 1)
            {
                Console.WriteLine("Serial\tName\tT.Price\tQuantity");

                foreach (KeyValuePair<int, Item> ShowCard in MyCard)
                {

                    Console.WriteLine(ShowCard.Key + "\t" + ShowCard.Value.ItemName + "\t" + ShowCard.Value.ItemPrice * Buy[ShowCard.Key] + "\t" + Buy[ShowCard.Key]);
                    TotalPrice += ShowCard.Value.ItemPrice*Buy[ShowCard.Key];
                }
                Console.WriteLine("You Need to Pay: "+TotalPrice+"TK.");
            }
            else
            {
                Console.WriteLine("Sorry Wrong Choice . Try Again");
                MoreBuy();
            }
        }
    }
}
