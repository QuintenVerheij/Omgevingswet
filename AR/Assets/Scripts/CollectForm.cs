using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectForm : MonoBehaviour
{
    Form formCollector;
    void Start() {
        formCollector = new Form();
    }
    public void InputHandler(int which, string input) {
        if (which==0) {
            formCollector.Username = input;
        }
        else if (which==1) {
            formCollector.Password = input;
        }
        else if (which==2) {
            formCollector.Email = input;
        }
        else if (which==3) {
            formCollector.Address.City = input;
        }
        else if (which==4) {
            formCollector.Address.Street = input;
        }
        else if (which==5) {
            formCollector.Address.Number = input;
        }
        else if (which==6) {
            formCollector.Address.NumExt = input;
        }
    }
}
public class Form {
    public string Username;
    public string Password;
    public string Email;
    public Address Address;
    public Form () {
        Username = "";
        Password = "";
        Email = "";
        Address = new Address();
    }
}
public class Address {
    public string City;
    public string Street;
    public string Number;
    public string NumExt;
    public Address () {
        City = "";
        Street = "";
        Number = "";
        NumExt = "";
    }
}
