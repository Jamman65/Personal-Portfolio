#pragma once
#include "TicketManager.h"
class Show :
    public TicketManager
{
public:
    void ShowSelection(int choice, int showPrice, int row);
    Show(int choice, int showPrice, int row, string GenreChoice);
    void Display();//Overloading from abstract class
    void GenreSelection(int choice, string GenreChoice);
    ~Show();

private: //Encapsulation
    //Rap Shows
    string RapshowName[4] = { "Future", "Drake", "Josh A", "Travis Scott" };
    string RapshowDate[4] = { "14/06/2022", "07/05/2022", "09/11/2022", "20/01/2023" };
    string RapshowTime[4] = { "19:00", "21:00", "12:00", "15:00" };
    string RapshowLocation[4] = { "Manchester", "Liverpool", "Birmingham", "Bury" };

    //Pop Shows 
    string PopshowName[4] = { "Rhianna", "Taylor Swift", "Beyonce", "Ed Sheeran" };
    string PopshowDate[4] = { "10/03/2023", "07/01/2022", "05/5/2022", "01/01/2023" };
    string PopshowTime[4] = { "19:00", "21:00", "12:00", "15:00" };
    string PopshowLocation[4] = { "Manchester", "Liverpool", "Birmingham", "Bury" };

    //Rock Shows
    string RockshowName[4] = { "Jake Hill", "ACDC", "U2", "Rolling Stones" };
    string RockshowDate[4] = { "2/06/2022", "04/03/2023", "07/11/2022", "10/01/2023" };
    string RockshowTime[4] = { "19:00", "21:00", "12:00", "15:00" };
    string RockshowLocation[4] = { "Manchester", "Liverpool", "Birmingham", "Bury" };


    int choice;
    static int showPrice; //Static Member for show price
    int row;
};

