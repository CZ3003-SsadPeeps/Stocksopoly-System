using System.Collections.Generic;

/// <summary>
/// Created by Khairuddin Bin Ali
/// </summary>
internal class NameInputController
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
        StockStore.LoadPurchaseRecords();
    }

    private bool ValidateName(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }
}
