using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Campus
/// </summary>
public class Campus
{
    private string id;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
	public Campus(string id, string name)
	{
        this.Id = id;
        this.Name = name;
	}
}