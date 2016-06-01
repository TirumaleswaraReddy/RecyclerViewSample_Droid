using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace RecyclerViewSample
{
	[Activity (Label = "RecyclerViewSample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		RecyclerView recyclerView;
		RecyclerView.LayoutManager layoutManager;
		RecyclerAdapter adapter;
		List<string> items;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			recyclerView = FindViewById<RecyclerView> (Resource.Id.recyclerView);
			 
			// improve performance if you know that changes in content
			// do not change the size of the RecyclerView
			recyclerView.HasFixedSize = true;

			// use a linear layout manager
			layoutManager = new LinearLayoutManager (this);
			recyclerView.SetLayoutManager (layoutManager);

			items = new List<string> ();
			for (int i = 0; i <= 1000; i++)
			{
				var item = string.Format ("Item No {0} #", i+1);
				items.Add (item);
			}

			// specify an adapter
			adapter = new RecyclerAdapter (items.ToArray());

			adapter.ItemClick += Adapter_ItemClick;
			adapter.ItemLongClick += Adapter_ItemLongClick;
			recyclerView.SetAdapter (adapter);
		}

		void Adapter_ItemLongClick (object sender, int e)
		{
			adapter.ToggleSelection (e);
			var position = e + 1;
			Toast.MakeText (this, "Long Pressed on Item No : " + position, ToastLength.Short).Show ();
		}

		void Adapter_ItemClick (object sender, int e)
		{
			adapter.ToggleSelection (e);
			var position = e + 1;
			Toast.MakeText (this, "Clicked on Item No : " + position, ToastLength.Short).Show ();
		}
	}
}


