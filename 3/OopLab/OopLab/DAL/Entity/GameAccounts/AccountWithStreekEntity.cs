using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab.DB.Entity;

namespace Lecture_23_10_2023_Alt.DB.Entity.GameAccounts
{
    // Підклас класу GameAccountEntity, представляє об'єкт облікового запису гравця (AccountWithStreekEntity) 
    // з додатковою інформацією про стійкість гравця в іграх (наприклад, стійкість до перемог або поразок)
    public class AccountWithStreekEntity : GameAccountEntity
    {
        // Цей клас успадковує всі властивості та методи базового класу GameAccountEntity
        // і може бути використаний для розширення функціональності облікового запису гравця
        // шляхом додавання нових полів чи методів, які визначають стійкість гравця в іграх.
    }
}