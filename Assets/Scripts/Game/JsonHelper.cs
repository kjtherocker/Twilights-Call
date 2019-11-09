﻿using System.Collections;
using System.Collections.Generic;
using System;

public static class JsonHelper
{

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    public static T ParseEnum<T>(string aText)
    {
        return (T)System.Enum.Parse(typeof(T), aText);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}