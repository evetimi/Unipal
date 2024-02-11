using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Event = Google.Apis.Calendar.v3.Data.Event;

public class CalendarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Authenticate using OAuth2
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "YOUR_CLIENT_ID",
                    ClientSecret = "YOUR_CLIENT_SECRET"
                },
                new[] { CalendarService.Scope.Calendar },
                "user",
                System.Threading.CancellationToken.None).Result;

            // Create Calendar Service
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Test"
            });

            // Define parameters for the event
            Event newEvent = new Event()
            {
                Summary = "Sample Event",
                Location = "Sample Location",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Now.AddHours(1),
                    TimeZone = "Your Time Zone"
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Now.AddHours(2),
                    TimeZone = "Your Time Zone"
                }
            };

            // Insert the event
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary");
            Event createdEvent = request.Execute();
            Debug.Log($"Event created: {createdEvent.HtmlLink}");
    }
}
