﻿using System;
using System.Collections.Generic;

class NameInputController
{
    internal List<NameValidationError> SubmitNames(string[] names)
    {
        List<NameValidationError> results = new List<NameValidationError>();

        string name;
        int i, j;
        for (i = 0; i < names.Length; i++)
        {
            name = names[i];

            // Check if name is blank
            if (!ValidateName(name))
            {
                results.Add(new NameValidationError.IsBlank(i));
                continue;
            }

            // Check if someone else has the same name
            for (j = i - 1; j >= 0; j--)
            {
                if (name != names[j]) continue;

                results.Add(new NameValidationError.Clash(i, j));
                break;
            }
        }

        return results;
    }

    internal void InitializeGame(string[] names)
    {
        GameStore.InitPlayers(names);
        NewsStore.ResetNews();

        if (!StockStore.IsStockLoaded())
        {
            StockStore.LoadStocks();
        }

        StockStore.LoadPurchaseRecords();
    }

    bool ValidateName(string name)
    {
        return !String.IsNullOrWhiteSpace(name);
    }
}
