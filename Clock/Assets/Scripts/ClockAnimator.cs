using UnityEngine;
using System;

public class ClockAnimator : MonoBehaviour {
  // Transforms for our clock's arms
  public Transform hours, minutes, seconds;

  // Transforms for each clock arm
  private const float hoursToDegrees = 360f / 12f;
  private const float minutesToDegrees = 360f / 60f, secondsToDegrees = 360f / 60f;

  // Support analog time
  public bool analog;

  void Start () {

  }

  void Update () {
    if (analog) {
      TimeSpan timespan = DateTime.Now.TimeOfDay;
      hours.localRotation = Quaternion.Euler(0f, 0f, (float)timespan.TotalHours * -hoursToDegrees);
      minutes.localRotation = Quaternion.Euler(0f, 0f, (float)timespan.TotalMinutes * -minutesToDegrees);
      seconds.localRotation = Quaternion.Euler(0f, 0f, (float)timespan.TotalSeconds * -secondsToDegrees);
    } else {
      DateTime time = DateTime.Now;
      hours.localRotation = Quaternion.Euler(0f, 0f, time.Hour * -hoursToDegrees);
      minutes.localRotation = Quaternion.Euler(0f, 0f, time.Minute * -minutesToDegrees);
      seconds.localRotation = Quaternion.Euler(0f, 0f, time.Second * -secondsToDegrees);      
    }

  }
}
