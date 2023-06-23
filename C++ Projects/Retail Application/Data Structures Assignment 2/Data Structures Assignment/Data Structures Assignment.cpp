#include <iostream>
#include "HashTable.h"
#include "Item.h"
#include "CustomerShoppingCart.h"
#include "Product.h"
#include "LinkedList.h"
#include "ProductTree.h"
#include "Category.h"
#include "Customer.h"

int main()
{
    bool MenuActivated = true;
    int Choice;
    int ProductChoice;

    //Initalise all of the products that are stored within the product table
    //Allowing the customer to add the products to their cart
    Product* product1 = new Product(1, "Jeans", 20, "Navy Blue");
    Product* product2 = new Product(2, "T-Shirt, Red", 15, "Red");
    Product* product3 = new Product(3, "Shoes", 25, "Sport");
    Product* product4 = new Product(4, "Jumper", 30, "White");
    Product* product5 = new Product(5, "Jacket", 40, "Black");
    HashTable* table = new HashTable(5); //Creates a new hash table to add all of the products to which can be viewed by each user
    ProductTree* productTree = new ProductTree(); //Creates a new product tree to add all of the categories to 
    CustomerShoppingCart* cart = new CustomerShoppingCart(); //Creates a new cart for each customer
    Customer* customer = new Customer("", "", "", ""); //Assigns a blank customer for a new customer to register
  

    //Initalise all of the products for the product table (Hash table)
    table->Insert(new Item(1, "Jeans", "Navy Blue", "5 Stars", 20));
    table->Insert(new Item(2, "T-Shirt", "Red Hipster Style", "4 Stars", 15));
    table->Insert(new Item(3, "Shoes", "Sport", "5 Stars", 25));
    table->Insert(new Item(4, "Jumper", "White", "4 Stars", 30));
    table->Insert(new Item(5, "Jacket", "Black - WaterProof", "3.5 Stars", 40));

    //Initalise all of the products to be added to the product tree 
    //Add all of the categories to the product tree
    productTree->Insert("-All Categories", "", product2);

    productTree->Insert("|- Clothing", "", product2);
    productTree->Insert("||- Men's Clothing", "", product1);
    productTree->Insert("|||- Shirts", "", product2);
    productTree->Insert("|||- Pants", "", product1);
    productTree->Insert("|||- FootWear", "", product2);
    productTree->Insert("|||- Jumpers", "", product1);
    productTree->Insert("|||- Acessories", "", product2);
    productTree->Insert("||- Womens Clothing", "", product1);


    //A switch menu has been created to allow a customer to:
    //Create an account or login
    //Display customer details
    //View the product inventory (product hash table)
    //View the product categories (product tree)
    //Add the products from the hash table to their shopping cart
    //Purchase selected products



 
    //Sets up a main menu for each user to allow the user to easily access every feature of the program
    //Product table(HashTable)
    //Product tree(Tree)
    //Customer shopping cart(LinkedList)
    while (MenuActivated == true) {
        system("cls");
        cout << " ---- Main Menu ----" << endl;
        cout << "\n" << "Login/Register (0)" << endl;
        cout << "\n" << "View Product Tree(Categories)(1)" << endl;
        cout << "\n" << "View Product Table(2)" << endl;
        cout << "\n" << "Add Products To Cart (3)" << endl;
        cout << "\n" << "Display Cart (4)" << endl;
        cout << "\n" << "Display Customer Details (5)" << endl;
        cout << "\n" << "Purchase Products (6)" << endl;
        cout << "\n" << "Quit (7)" << endl;

        cin >> Choice;

        switch (Choice) {
        case 0:
            //Allows the customer to create their profile with all of their details and payment details
            system("cls");
            customer->Initialise();
            customer->GetCustomerProfile("", "", "", "");
            customer->GetLogin("", "", ',');
            customer->Display(customer);
            customer->GetTransactionDetails("", "", "", '.'); //sets all the values to blank for the customer to enter their own details
            system("pause");
            break;

        case 1:
          
            //Product Tree
            //Displays all of the categories for each product within the product table 
            system("cls");
            productTree->DisplayInOrder();
            cout <<"Each category is shown within the product table showing all of the stores inventory" << endl;
            system("pause");
            break;
        case 2:
            //Hash Table
            //create a hash table for product details
            //Shows all of the products for that the shop contains for each customer with the relevant details
            //the product tree will show all the category details of each product created within the hash table
            system("cls");
            cout << "Product Table:" << endl;


            table->Display();
            system("pause");
            break;
        case 3:

            //Linked List
             //Allows for a customer to add new products to their shopping cart from the hash table

            //Add all the items currently stored in the product table to the choices to allow the customer to add them to the cart
            system("cls");
            cout << "Product Table:" << endl;


            table->Display();

            cout << "Which products would you like to add to your cart?" << endl;
            cout << "Choose the products via the index number" << endl;
            cin >> ProductChoice;
            
            if (ProductChoice == 0) {
                cart->AddProduct(product5);
                
            }
            if (ProductChoice == 1) {
                cart->AddProduct(product1);
            }

            if (ProductChoice == 2) {
                cart->AddProduct(product2);
            }

            if (ProductChoice == 3) {
                cart->AddProduct(product3);
            }

            if (ProductChoice == 4) {
                cart->AddProduct(product4);
            }
   
       

            system("pause");


            break;
        case 4:
            system("cls");
            cart->DisplayCart();
            system("pause");
            break;
        case 5:
            //Displays all of the customers details that they have registered with
            system("cls");
            customer->Display(customer);
            system("pause");
            break;


         
        case 6:
            //Allows the customer to purchase all of the items within their cart
            system("cls");
            cart->DisplayCart();
            customer->DisplayTransaction("", "", "", '.');
            cart->ClearProducts(); //Clears the customers cart for their next purchase
       
            break;
        case 7:
            //Breaks the switch loop
            MenuActivated = false;

        }


    }
}
