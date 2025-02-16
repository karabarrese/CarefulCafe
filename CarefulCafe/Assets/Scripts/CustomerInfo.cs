using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Allergy
{
    None, Dairy, Gluten, Egg
}

[System.Serializable]  // Ensure CustomerInfo is serializable
public class CustomerInfo
{
    public string Name;
    public Allergy CusAllergy;
    public Sprite TextImg;
    public string SeverityStatement;
}