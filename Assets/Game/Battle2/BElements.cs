using UnityEngine;
using System.Collections;

public class BElements {


    public enum Elements { Normal, Fire, Grass, Rock, Electric, Water };

    public static float effectiveThreshold = 0.5f;
    private static float[,] multiplier = new float[6, 6]{
    // Attacker                     Defender
    //            Normal, Fire, Grass, Rock, Electric, Water
	/*Normal*/    {	0.5f,	0.6f,   0.6f,   0.6f,   0.6f,   0.6f},
	/*Fire*/      {	0.4f,	0.2f,	0.7f,	0.3f,	0.5f,	0.8f},
	/*Grass*/     {	0.4f,	0.2f,	0.2f,	0.7f,	0.5f,	0.8f},
	/*Rock*/      {	0.4f,	0.8f,	0.3f,	0.2f,	0.7f,	0.5f},
	/*Electric*/  {	0.4f,	0.7f,	0.3f,	0.2f,	0.5f,	0.8f},
	/*Water*/     {	0.4f,	0.8f,	0.3f,	0.7f,	0.5f,	0.2f}
    };


    public static float getMultiplier(Elements attacker, Elements defender)
    {
        return getMultiplier((int)attacker, (int)defender);
    }
    public static float getMultiplier(int attacker, int defender)
    {
        return multiplier[attacker, defender];
    }


    public static float[] TranslateDamage(float[] damages, Elements defender)
    {
        return TranslateDamage(damages, (int)defender);
    }
    public static float[] TranslateDamage(float[] damages, int defender)
    {
        float[] realDmg = new float[6];
        for (int i = 0; i < 6; i++)
        {
            realDmg[i] = getMultiplier(i, (int)defender) * damages[i];
        }
        return realDmg;
    }
    public static bool isEffective(Elements attacker, Elements defender)
    {
        return isEffective((int)attacker, (int)defender);
    }
    public static bool isEffective(int attacker, int defender)
    {
        return multiplier[attacker, defender] > effectiveThreshold;
    }

}
