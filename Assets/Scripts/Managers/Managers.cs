using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Managers
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static T GetService<T>() where T : class, new()
    {
        var type = typeof(T);
        if(services.ContainsKey(type))
        {
            return (T)services[type];
        }
        else
        {
            var service = new T();
            services.Add(type, service);

            return service;
        }

        throw new Exception("Service not found : " + type);
    }
}