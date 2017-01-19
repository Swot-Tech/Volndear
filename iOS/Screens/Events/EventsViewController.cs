using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Volndear.iOS
{
	public partial class EventsViewController : UIViewController
	{
		UIView vwContainer = new UIView();
	
		UIScrollView svEvents = new UIScrollView();
		public UITableView tvEventsTab;
		public List<Events> eventsList = new List<Events>();
		nfloat eventContainerPadding = 10;
		UIView vwEvents = new UIView();

		UIScrollView svAddNewEventDetails = new UIScrollView();
		UIView vwNewEventBtn = new UIView();
		public EventsViewController() : base("EventsViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.NavigationItem.SetHidesBackButton(true, true);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(7, 198, 192);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			AutomaticallyAdjustsScrollViewInsets = false;
			svEvents.ContentInset = UIEdgeInsets.Zero; //UIEdgeInsetsMake(0, 0, 0, 0);

			configureEventsView();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void configureEventsView()
		{
			vwNewEventBtn.Hidden = false;
			Title = "Events around you";
			
			this.NavigationItem.LeftBarButtonItem = null;
			this.NavigationItem.RightBarButtonItem = null;
			int count = 1;
			eventsList = new List<Events>();
			eventsList = AppDelegate.eventDatabase.GetAllEvents();
		    eventsList.Reverse();

			var screen = UIScreen.MainScreen.Bounds;
			vwContainer = new UIView();
			vwContainer.Frame = new CGRect(0,60, screen.Width, screen.Height-110);
			//vwContainer.BackgroundColor = UIColor.Magenta;
			View.AddSubview(vwContainer);

			if (eventsList.Count == 0)
			{
				UILabel lblNoEvents = new UILabel();
				lblNoEvents.Frame = new CGRect(50, 200, vwContainer.Frame.Width - 20, 50);
				lblNoEvents.Text = "OOPS ! , No Events found. ";
				vwContainer.AddSubview(lblNoEvents);

			}
			else
			{
				svEvents.Frame = new CGRect(0, 0, screen.Width , vwContainer.Frame.Height);
				svEvents.BackgroundColor = UIColor.LightGray;
				//svEvents.Layer.CornerRadius = 5;
				//svEvents.ClipsToBounds = true;
				vwContainer.AddSubview(svEvents);

				foreach (Events evet in eventsList)
				{
					if (count == 1)
					{
						
					}
					else
					{
						eventContainerPadding = vwEvents.Frame.Bottom + 5;
					}
					ConfigureEventScrollview(evet);
					count++;
				}

			}


			vwNewEventBtn.Frame = new CGRect(screen.Width - 80, screen.Bottom - 120, 50, 50);
			vwNewEventBtn.BackgroundColor = UIColor.FromRGB(7, 198, 192);
			vwNewEventBtn.Layer.CornerRadius = 25;
			vwNewEventBtn.ClipsToBounds = true;
			View.AddSubview(vwNewEventBtn);

			UIButton btnAddNewEvent = new UIButton();
			btnAddNewEvent.Frame = new CGRect(0, -3, 50, 50);
			//btnAddNewEvent.Layer.CornerRadius = 25;
			//btnAddNewEvent.ClipsToBounds = true;
			btnAddNewEvent.SetTitle("+", UIControlState.Normal);
			btnAddNewEvent.TitleLabel.TextAlignment = UITextAlignment.Natural;
			btnAddNewEvent.Font = UIFont.FromName("Helvetica", 40);
			btnAddNewEvent.SetTitleColor(UIColor.White, UIControlState.Normal);
			//btnAddNewEvent.BackgroundColor = UIColor.FromRGB(7, 198, 192);
			vwNewEventBtn.AddSubview(btnAddNewEvent);


			btnAddNewEvent.TouchUpInside += (sender, e) =>
				{
					NavigationController.PushViewController(new AddNewEventViewController(), true);
				};
		}


		private void ConfigureEventScrollview(Events eve)
		{
			//var screen = UIScreen.MainScreen.Bounds;
		
		
			vwEvents = new UIView();
			vwEvents.Frame = new CGRect(10,eventContainerPadding, svEvents.Frame.Width - 20, 200);
			vwEvents.BackgroundColor = UIColor.White;
			vwEvents.Layer.CornerRadius = 5;
			vwEvents.ClipsToBounds = true;
			svEvents.AddSubview(vwEvents);


			UIImageView imgEvent = new UIImageView();
			imgEvent.Frame = new CGRect(05, 08, 30, 30);
			imgEvent.Image = UIImage.FromFile("Images/blood_donation.png");
			imgEvent.Layer.CornerRadius = 5;
			imgEvent.ClipsToBounds = true;
			vwEvents.AddSubview(imgEvent);

			UILabel lblEventsName = new UILabel(new CGRect(imgEvent.Frame.Width + 10, 10, vwEvents.Frame.Width - 25, 30));
			lblEventsName.Text = eve.EventName;
			lblEventsName.TextColor = UIColor.Black;
			lblEventsName.TextAlignment = UITextAlignment.Left;
			lblEventsName.Font = UIFont.FromName("Helvetica-Bold", 14);
			lblEventsName.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventsName.Lines = 0;
			//lblEventsName.SizeToFit();
			vwEvents.AddSubview(lblEventsName);


			UITapGestureRecognizer labelTap = new UITapGestureRecognizer(() =>
			{
				configureEventDetailsView(eve);
			});

			lblEventsName.UserInteractionEnabled = true;
			lblEventsName.AddGestureRecognizer(labelTap);

			UILabel lblTimeSpan = new UILabel();
			lblTimeSpan.Frame = new CGRect(vwEvents.Frame.Width - 70, lblEventsName.Frame.Y, 70, 30);
			//TimeSpan span = System.DateTime.Now .Subtract(eve.EventPostedTime);
			if (eve.EventPostedTime.Date == System.DateTime.Today)
			{
				lblTimeSpan.Text = "Today";
			}
			else
			{
				lblTimeSpan.Text = eve.EventPostedTime.ToString("dd/MMM");
			}
			lblTimeSpan.TextColor = UIColor.Gray;
			lblTimeSpan.Font = UIFont.FromName("Helvetica", 12);
			vwEvents.AddSubview(lblTimeSpan);

			UILabel lblEventOrganizerName = new UILabel(new CGRect(imgEvent.Frame.Width + 12, lblEventsName.Frame.Bottom - 14, 100, 30));
			lblEventOrganizerName.Text = "by " + eve.EventOrganizerName;
			lblEventOrganizerName.TextColor = UIColor.Gray;
			lblEventOrganizerName.TextAlignment = UITextAlignment.Left;
			lblEventOrganizerName.Font = UIFont.FromName("Helvetica", 12);
			lblEventOrganizerName.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventOrganizerName.Lines = 0;
			vwEvents.AddSubview(lblEventOrganizerName);

			UILabel lblEventDateTime = new UILabel(new CGRect(10, lblEventOrganizerName.Frame.Bottom - 5, 200, 30));
			lblEventDateTime.Text = eve.EventDateTime.ToString("MMM d, yyyy");

			lblEventDateTime.TextColor = UIColor.Black;
			lblEventDateTime.TextAlignment = UITextAlignment.Left;
			lblEventDateTime.Font = UIFont.FromName("Helvetica-Bold", 12);

			vwEvents.AddSubview(lblEventDateTime);


			UILabel lblEventDescription = new UILabel(new CGRect(10, lblEventDateTime.Frame.Bottom, vwEvents.Frame.Width- 20, 40));

			lblEventDescription.TextColor = UIColor.Black;
			lblEventDescription.TextAlignment = UITextAlignment.Left;
			lblEventDescription.Font = UIFont.FromName("Helvetica", 12);
			lblEventDescription.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventDescription.Lines = 0;
			if (eve.EventDescription.Length > 81)
			{
				eve.EventDescription = eve.EventDescription.Substring(0, 80) + "...";

			}

			lblEventDescription.Text = eve.EventDescription;


			vwEvents.AddSubview(lblEventDescription);



			UILabel lblEventAddrees = new UILabel(new CGRect(10, lblEventDescription.Frame.Bottom + 10, vwEvents.Frame.Width, 30));
			lblEventAddrees.Text = eve.EventAddress;
			lblEventAddrees.TextColor = UIColor.Gray;
			lblEventAddrees.TextAlignment = UITextAlignment.Left;
			lblEventAddrees.Font = UIFont.FromName("Helvetica", 12);
			lblEventAddrees.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventAddrees.Lines = 0;
			lblEventAddrees.SizeToFit();
			vwEvents.AddSubview(lblEventAddrees);


			UIImageView imgJoined = new UIImageView();
			imgJoined.Frame = new CGRect(10, lblEventAddrees.Frame.Bottom + 10, 15, 15);
			imgJoined.Image = UIImage.FromFile("Images/Joined_image.png");
			imgJoined.Layer.CornerRadius = 0;
			imgJoined.ClipsToBounds = true;
			vwEvents.AddSubview(imgJoined);


			UILabel lblJoined = new UILabel(new CGRect(imgJoined.Frame.Width + 20, lblEventAddrees.Frame.Bottom + 3, 100, 30));
			lblJoined.Text = "7 Joined";
			lblJoined.TextColor = UIColor.Gray;
			lblJoined.TextAlignment = UITextAlignment.Left;
			lblJoined.Font = UIFont.FromName("Helvetica", 12);
			vwEvents.AddSubview(lblJoined);


			UIView vwMapBtn = new UIView();
			vwMapBtn.Frame = new CGRect(lblEventDateTime.Frame.Width, lblEventDateTime.Frame.Top - 5, 70, 30);
			vwMapBtn.BackgroundColor = UIColor.FromRGB(204, 232, 231);
			vwMapBtn.Layer.CornerRadius = 5;
			vwMapBtn.ClipsToBounds = true;
			vwEvents.AddSubview(vwMapBtn);


			UIButton btnShowInMapImage = new UIButton();
			btnShowInMapImage.SetImage(UIImage.FromFile("Images/marker.png"), UIControlState.Normal);
			//btnShowInMapImage.Frame = new CGRect(lblEventDateTime.Frame.Width, lblEventDateTime.Frame.Top - 5, 30, 30);
			btnShowInMapImage.Frame = new CGRect(3, 5, 20, 20);
			vwMapBtn.AddSubview(btnShowInMapImage);

			UIButton btnShowInMap = new UIButton();
			//btnShowInMap.BackgroundColor = UIColor.FromRGB(7, 198, 192);
			btnShowInMap.SetTitle("Show in map", UIControlState.Normal);
			btnShowInMap.Font = UIFont.FromName("Helvetica", 9);
			btnShowInMap.SetTitleColor(UIColor.FromRGB(7, 198, 192),UIControlState.Normal);
			//btnShowInMap.Frame = new CGRect(btnShowInMapImage.Frame.X, btnShowInMapImage.Frame.Top , 50, 30);
			btnShowInMap.Frame = new CGRect(btnShowInMapImage.Frame.Width+2, 1, 40, 30);
			btnShowInMap.TitleLabel.Lines = 0;
			btnShowInMap.TitleLabel.TextAlignment = UITextAlignment.Center;
			btnShowInMap.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
			vwMapBtn.AddSubview(btnShowInMap);


			svEvents.ContentSize = new CGSize(svEvents.Frame.Width, vwEvents.Frame.Bottom +1);
		//	svEvents.ScrollRectToVisible(new CGRect(0, 0, svEvents.ContentSize.Width - 1, svEvents.Frame.Top +1), true);

		}


		private void configureEventDetailsView(Events eventToShow)
		{
			foreach (var subView in vwContainer.Subviews)
			{
				subView.RemoveFromSuperview();
			}

			vwNewEventBtn.Hidden = true;
			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, args) =>
					{
				foreach (var subView in vwContainer.Subviews)
						{
							subView.RemoveFromSuperview();
						}
						configureEventsView();
					})
				, true);
			Title = eventToShow.EventName;
			var screen = UIScreen.MainScreen.Bounds;
			vwContainer = new UIView();
			vwContainer.Frame = new CGRect(0, 50, screen.Width, screen.Height-70);
			View.AddSubview(vwContainer);


			UIImageView imgEventImage = new UIImageView();
			imgEventImage.Frame = new CGRect(10, 10, vwContainer.Frame.Width - 20, 200);
		//	imgEventImage.Image = UIImage.FromFile(eventToShow.vwNewEventContainerEventImageLink);
			imgEventImage.Image = UIImage.FromFile("Images/blood_donation.png");
			imgEventImage.Layer.CornerRadius = 5;
			imgEventImage.ClipsToBounds = true;
			vwContainer.AddSubview(imgEventImage);

			UILabel lblEventName = new UILabel(new CGRect(20, imgEventImage.Frame.Bottom - 40, imgEventImage.Frame.Width - 25, 30));
			lblEventName.Text = eventToShow.EventName;
			lblEventName.TextColor = UIColor.White;
			lblEventName.TextAlignment = UITextAlignment.Left;
			lblEventName.Font = UIFont.FromName("Helvetica-Bold", 16);
			lblEventName.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventName.Lines = 0;
			//lblEventsName.SizeToFit();
			vwContainer.AddSubview(lblEventName);

			UIImageView imgEvent = new UIImageView();
			imgEvent.Frame = new CGRect(10, imgEventImage.Frame.Bottom +10, 30, 30);
			imgEvent.Image = UIImage.FromFile("Images/blood_donation.png");
			imgEvent.Layer.CornerRadius = 5;
			imgEvent.ClipsToBounds = true;
			vwContainer.AddSubview(imgEvent);

			UILabel lblEventsName = new UILabel(new CGRect(imgEvent.Frame.Width + 13, imgEventImage.Frame.Bottom +8, vwEvents.Frame.Width - 25, 30));
			lblEventsName.Text = eventToShow.EventName;
			lblEventsName.TextColor = UIColor.Black;
			lblEventsName.TextAlignment = UITextAlignment.Left;
			lblEventsName.Font = UIFont.FromName("Helvetica-Bold", 14);
			lblEventsName.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventsName.Lines = 0;
			//lblEventsName.SizeToFit();
			vwContainer.AddSubview(lblEventsName);


			UILabel lblEventOrganizerName = new UILabel(new CGRect(imgEvent.Frame.Width + 15, lblEventsName.Frame.Bottom - 14, 100, 30));
			lblEventOrganizerName.Text = "by " + eventToShow.EventOrganizerName;
			lblEventOrganizerName.TextColor = UIColor.Gray;
			lblEventOrganizerName.TextAlignment = UITextAlignment.Left;
			lblEventOrganizerName.Font = UIFont.FromName("Helvetica", 12);
			lblEventOrganizerName.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventOrganizerName.Lines = 0;
			vwContainer.AddSubview(lblEventOrganizerName);

			UILabel lblEventDateTime = new UILabel(new CGRect(10, lblEventOrganizerName.Frame.Bottom - 5, 200, 30));
			lblEventDateTime.Text = eventToShow.EventDateTime.ToString("MMM d, yyyy");
			lblEventDateTime.TextColor = UIColor.Black;
			lblEventDateTime.TextAlignment = UITextAlignment.Left;
			lblEventDateTime.Font = UIFont.FromName("Helvetica-Bold", 12);

			vwContainer.AddSubview(lblEventDateTime);


			UILabel lblEventDescription = new UILabel(new CGRect(10, lblEventDateTime.Frame.Bottom -10, vwContainer.Frame.Width, 40));
			lblEventDescription.TextColor = UIColor.Black;
			lblEventDescription.TextAlignment = UITextAlignment.Left;
			lblEventDescription.Font = UIFont.FromName("Helvetica", 12);
			lblEventDescription.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventDescription.Lines = 0;
			lblEventDescription.SizeToFit();
			lblEventDescription.Text = eventToShow.EventDescription;
			vwContainer.AddSubview(lblEventDescription);


			UILabel lblEventAddrees = new UILabel(new CGRect(10, lblEventDescription.Frame.Bottom , vwContainer.Frame.Width, 30));
			lblEventAddrees.Text = eventToShow.EventAddress;
			lblEventAddrees.TextColor = UIColor.Gray;
			lblEventAddrees.TextAlignment = UITextAlignment.Left;
			lblEventAddrees.Font = UIFont.FromName("Helvetica", 12);
			lblEventAddrees.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventAddrees.Lines = 0;
			lblEventAddrees.SizeToFit();
			vwContainer.AddSubview(lblEventAddrees);

			UILabel lblEventOrganizerContactNumber = new UILabel(new CGRect(10, lblEventAddrees.Frame.Bottom +10, vwContainer.Frame.Width, 30));
			lblEventOrganizerContactNumber.Text = "Contact : " + eventToShow.EventOrganizerContactNumber;
			lblEventOrganizerContactNumber.TextColor = UIColor.Gray;
			lblEventOrganizerContactNumber.TextAlignment = UITextAlignment.Left;
			lblEventOrganizerContactNumber.Font = UIFont.FromName("Helvetica", 12);
			lblEventOrganizerContactNumber.LineBreakMode = UILineBreakMode.WordWrap;
			lblEventOrganizerContactNumber.Lines = 0;
			lblEventOrganizerContactNumber.SizeToFit();
			vwContainer.AddSubview(lblEventOrganizerContactNumber);

			UIView vwMapBtn = new UIView();
			vwMapBtn.Frame = new CGRect(lblEventDateTime.Frame.Width+20, lblEventDateTime.Frame.Top - 5, 70, 30);
			vwMapBtn.BackgroundColor = UIColor.FromRGB(204, 232, 231);
			vwMapBtn.Layer.CornerRadius = 5;
			vwMapBtn.ClipsToBounds = true;
			vwContainer.AddSubview(vwMapBtn);


			UIButton btnShowInMapImage = new UIButton();
			btnShowInMapImage.SetImage(UIImage.FromFile("Images/marker.png"), UIControlState.Normal);
			//btnShowInMapImage.Frame = new CGRect(lblEventDateTime.Frame.Width, lblEventDateTime.Frame.Top - 5, 30, 30);
			btnShowInMapImage.Frame = new CGRect(3, 5, 20, 20);
			vwMapBtn.AddSubview(btnShowInMapImage);

			UIButton btnShowInMap = new UIButton();
			//btnShowInMap.BackgroundColor = UIColor.FromRGB(7, 198, 192);
			btnShowInMap.SetTitle("Show in map", UIControlState.Normal);
			btnShowInMap.Font = UIFont.FromName("Helvetica", 9);
			btnShowInMap.SetTitleColor(UIColor.FromRGB(7, 198, 192), UIControlState.Normal);
			//btnShowInMap.Frame = new CGRect(btnShowInMapImagevwNewEventContainervwNewEventContainer.Frame.X, btnShowInMapImage.Frame.Top , 50, 30);
			btnShowInMap.Frame = new CGRect(btnShowInMapImage.Frame.Width + 2, 1, 40, 30);
			btnShowInMap.TitleLabel.Lines = 0;
			btnShowInMap.TitleLabel.TextAlignment = UITextAlignment.Center;
			btnShowInMap.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
			vwMapBtn.AddSubview(btnShowInMap);

			UIButton btnJoin = new UIButton();
			btnJoin.Frame = new CGRect(vwContainer.Frame.Width /2 -50, lblEventOrganizerContactNumber.Frame.Bottom +40, 100, 30);
			btnJoin.SetTitle("Join", UIControlState.Normal);
			btnJoin.BackgroundColor = UIColor.FromRGB(7, 198, 192);
			btnJoin.SetTitleColor(UIColor.White, UIControlState.Normal);
			vwContainer.AddSubview(btnJoin);
			btnJoin.SetTitle("Joined", UIControlState.Disabled);
			btnJoin.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);

		}

	}

}

