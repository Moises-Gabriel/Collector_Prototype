using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Journal : MonoBehaviour
{
    private GameObject _journal;
    private TextMeshProUGUI _journalEntry;
    public bool IsOpen;

    void Start()
    {
        _journal = GameObject.Find("Journal");
        _journalEntry = GameObject.Find("Journal Entry").GetComponent<TextMeshProUGUI>();
        _journal.SetActive(false);
    }

    void Update()
    {
        if (IsOpen)
            StartCoroutine(JournalPopup());
    }

    private string jEntry = String.Empty;
    IEnumerator JournalPopup()
    {
        _journal.SetActive(true);
        _journalEntry.text = jEntry;
        yield return new WaitForSeconds(1.5f);
        _journal.SetActive(false);
        IsOpen = false;
    }

    public string AddToJournal(string entry, bool isOpen)
    {
        jEntry = entry;
        IsOpen = isOpen;
        return entry;
    }
}
