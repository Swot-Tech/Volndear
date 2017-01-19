using System;
using CoreLocation;
using MapKit;
using UIKit;

namespace Volndear.iOS
{
	public partial class MapViewController : UIViewController
	{

		MKMapView mapView;
		CLLocationManager locationManager = new CLLocationManager();
		public MapViewController() : base("MapViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ConfigureMapScreen();
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(7, 198, 192);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void ConfigureMapScreen()
		{
			try
			{


				mapView = new MKMapView(View.Bounds);
				mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
				View.AddSubview(mapView);

				locationManager.RequestWhenInUseAuthorization();
				// this is required to show the blue dot indicating user-location
				mapView.ShowsUserLocation = true;
				mapView.SetCenterCoordinate(mapView.UserLocation.Coordinate, true);

				mapView.DidUpdateUserLocation += (sender, e) =>
				{
					if (mapView.UserLocation != null)
					{
						//Send(userName, mapView.UserLocation.Coordinate.Latitude, mapView.UserLocation.Coordinate.Longitude);
						Console.WriteLine("userloc:" + mapView.UserLocation.Coordinate.Latitude + "," + mapView.UserLocation.Coordinate.Longitude);
						CLLocationCoordinate2D coords = mapView.UserLocation.Coordinate;
						mapView.SetCenterCoordinate(mapView.UserLocation.Coordinate, true);
						//MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coords.Latitude));
					//	mapView.Region = new MKCoordinateRegion(coords, span);

					}
				};

				if (!mapView.UserLocationVisible)
				{
					// user denied permission, or device doesn't have GPS/location ability
					Console.WriteLine("userloc not visible, show cupertino");
					CLLocationCoordinate2D coords = new CLLocationCoordinate2D(13.0725358950059, 80.2477257793859);
					MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(20), MilesToLongitudeDegrees(20, coords.Latitude));
					mapView.Region = new MKCoordinateRegion(coords, span);
				}
			}
			catch (Exception ex)
			{ 
			
			}
		}
		public double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0;
			double radiansToDegrees = 180.0 / Math.PI;
			return (miles / earthRadius) * radiansToDegrees;
		}

		/// <summary>
		/// Converts miles to longitudinal degrees at a specified latitude
		/// </summary>
		public double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0;
			double degreesToRadians = Math.PI / 180.0;
			double radiansToDegrees = 180.0 / Math.PI;

			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
			return (miles / radiusAtLatitude) * radiansToDegrees;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}


	class BasicMapAnnotation : MKAnnotation
	{
		/// <summary>
		/// The location of the annotation
		/// </summary>
		CLLocationCoordinate2D coord;
		string title, subtitle;
		public string UserName { get; set; }

		public override CLLocationCoordinate2D Coordinate
		{
			get { return coord; }
		}
		public override void SetCoordinate(CLLocationCoordinate2D value)
		{
			coord = value;
		}

		/// <summary>
		/// The title text
		/// </summary>
		public override string Title
		{
			get { return title; }
		}

		/// <summary>
		/// The subtitle text
		/// </summary>
		public override string Subtitle
		{
			get { return subtitle; }
		}

		public BasicMapAnnotation(CLLocationCoordinate2D coordinate, string title, string subTitle)
			: base()
		{
			coord = coordinate;
			this.title = title;
			this.subtitle = subTitle;
		}


	}
}

