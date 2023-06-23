#pragma once
#include "TicketManager.h"
class Ticket :
    public TicketManager //Inheritence
{
public:
    int price; 
    void Display(); //Overloading from abstract class
    void PurchaseTicket( int price, string TicketType, string TicketChoice);
    Ticket(int price, string TicketType, string TicketChoice);
    ~Ticket();
    friend Ticket** Tickets(Ticket& ticket);
    

private: //Encapsulation
    
    string TicketType;
    string TicketChoice;
    


};

