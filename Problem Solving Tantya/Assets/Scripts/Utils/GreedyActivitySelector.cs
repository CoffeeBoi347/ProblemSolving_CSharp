using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GreedyActivitySelector : MonoBehaviour
{
    private void Start()
    {
        List<Activity> activities = new List<Activity>
        {
            new Activity("A", 5, 11),
            new Activity("B", 3, 5),
            new Activity("C", 0, 6),
            new Activity("D", 5, 7),
            new Activity("E", 3, 9),
            new Activity("F", 5, 9),
            new Activity("G", 6, 10),
            new Activity("H", 8, 11),
            new Activity("I", 8, 12),
        };

        activities = activities.OrderBy(a => a.End).ToList();
        List<Activity> selectedActivities = new List<Activity>();
        int lastFinish = int.MinValue;

        foreach (var activity in activities)
        {
            if (activity.Start >= lastFinish)
            {
                selectedActivities.Add(activity);
                lastFinish = activity.End;
            }
        }

        foreach (var selected in selectedActivities)
        {
            Debug.Log($"Selected Activity: {selected.Name} (Start: {selected.Start}, End: {selected.End})");
        }
    }
}
