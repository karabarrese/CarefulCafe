using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Allergy
{
    None, Dairy, Gluten, Egg
}

public class CustomerInfo
{
    public string Name { get; set; }
    public Allergy CusAllergy { get; set; }

    public CustomerInfo (string name, Allergy allergy){
        Name = name;
        CusAllergy = allergy;
    }
}
