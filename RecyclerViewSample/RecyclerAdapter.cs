
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using Android.Graphics;
using Android.Util;

namespace RecyclerViewSample
{
	[Activity (Label = "RecyclerAdapter")]			
	public class RecyclerAdapter : RecyclerView.Adapter
	{
		string [] items;
		public event EventHandler<int> ItemClick;
		public event EventHandler<int> ItemLongClick;
		private SparseBooleanArray _SelectedItemIds;
		private int _position;

		public RecyclerAdapter(string[] data)
		{
			items = data;
			_SelectedItemIds = new SparseBooleanArray();
		}

		#region implemented abstract members of Adapter

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			var item = items [position];

			// Replace the contents of the view with that element
			var viewHolder = holder as MyViewHolder;
			viewHolder.TextView.Text = items[position];

			if (_SelectedItemIds.Get(position))
			{                
				viewHolder.TextView.SetBackgroundColor(Color.ParseColor("#9EC0FF"));
			}
			else
			{
				viewHolder.TextView.SetBackgroundColor(Color.Transparent);
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			// set the view's size, margins, paddings and layout parameters
			var tv = new TextView (parent.Context);
			LinearLayout.LayoutParams loclayoutparams = new LinearLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, 100);
			tv.LayoutParameters = loclayoutparams;
			tv.SetTextColor (Color.Black);
			tv.TextSize = 20;
			tv.Gravity = GravityFlags.Center;

			tv.LongClick += (sender, e) => 
			{
				tv.SetBackgroundColor (Color.ParseColor ("#9EC0FF"));
				var a = tv.Text;
			};

			var vh = new MyViewHolder (tv, OnClick, OnLongClick);
			return vh;
		}

		public override int ItemCount {
			get {
				return items.Length;
			}
		}

		#endregion

		void OnClick (int position)
		{
			if (ItemClick != null)
				ItemClick (this, position);
			_position = position;
		}

		void OnLongClick (int position)
		{
			if (ItemLongClick != null)
				ItemLongClick (this, position);
			_position = position;
		}

		public class MyViewHolder : RecyclerView.ViewHolder
		{
			public TextView TextView { get; set; }


			public MyViewHolder (TextView v, Action<int> listener, Action<int> longListener) : base (v)
			{
				TextView = v;
				v.Click += (sender, e) => listener (base.Position);
				v.LongClick += (sender, e) => longListener (base.Position);
			}
		}

		public void ToggleSelection(int position)
		{
			SelectView(position, !_SelectedItemIds.Get(position));
		}

		public void SelectView(int position, bool value)
		{
			if(value)
				_SelectedItemIds.Put(position,value);
			else
				_SelectedItemIds.Delete(position);
			NotifyDataSetChanged();
		}
	}
}

