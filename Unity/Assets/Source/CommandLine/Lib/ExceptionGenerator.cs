using System;
using UnityEngine;
using System.Collections;

public class ExceptionGenerator {

    public string Generate()
    {
        try
        {
            throw new ArgumentException("wrong code!!!");
        }
        catch (Exception e)
        {
            return e.ToString();
        }

        return null;
    }

}
