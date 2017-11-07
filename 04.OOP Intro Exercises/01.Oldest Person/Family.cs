using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Family
{
    private List<Person> listOfPeople;

    public Family()
    {
        this.listOfPeople = new List<Person>();
    }

    public void AddMember(Person member)
    {
        this.listOfPeople.Add(member);  
    }

    public Person GetOldestMember(Family family)
    {
        var result= family
            .listOfPeople
            .OrderByDescending(p => p.Age)
            .First();
        return result;
    }
}

