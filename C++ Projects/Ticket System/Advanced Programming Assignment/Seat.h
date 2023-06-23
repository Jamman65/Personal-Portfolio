#pragma once
#include "TicketManager.h"
class Seat :
    public TicketManager
{
public:
    void Display();
    Seat(int row, int col);
    void SeatSelection(int choice, string SeatType);
    void SeatChoice();
    ~Seat();
    void SeatPlan(int row, int col);


private: //encapsulation 
    const int row = 5; 
    const int col = 7;
    char seatplan[5][7];
    char choice;
    int seatAmount = 0;
    string SeatType;

};

