using Android.App;
using Android.OS;
using Piller.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Piller.Resources;
using MvvmCross.Binding.BindingContext;
using Android.Support.Design.Widget;
using Android.Views;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.Droid.BindingContext;

namespace Piller.Droid.Views
{
	[Activity]
	public class MedicationSummaryListView : MvxAppCompatActivity<MedicationSummaryListViewModel>
	{


		FloatingActionButton newMedicationDosage;
		MvxListView medicationList;

		protected override void OnCreate(Bundle bundle)
		{

			base.OnCreate(bundle);
			SetContentView(Resource.Layout.MedicationSummaryListView);

			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			newMedicationDosage = FindViewById<FloatingActionButton>(Resource.Id.newMedicationDosage);

			medicationList = FindViewById<MvxListView>(Resource.Id.medicationList);
            medicationList.ItemTemplateId = Resource.Layout.medication_summary_item;
            medicationList.Adapter = new MedicationSummaryAdapter(this, (IMvxAndroidBindingContext)this.BindingContext);
            //Toolbar will now take on default actionbar characteristics
            SetSupportActionBar(toolbar);

			SupportActionBar.Title = AppResources.MedicationSummaryListViewModel_Title;

			SetBinding();
		}
        protected override void OnResume()
        {
            base.OnResume();
           // medicationList.Adapter = new MedicationSummaryAdapter(this, (IMvxAndroidBindingContext)this.BindingContext);


        }
        private void SetBinding()
		{
			var bindingSet = this.CreateBindingSet<MedicationSummaryListView, MedicationSummaryListViewModel>();

			bindingSet.Bind(newMedicationDosage)
					  .For(nameof(View.Click))
					  .To(x => x.AddNew);
            
			bindingSet.Bind(medicationList)
				.For(x => x.ItemsSource)
				.To(vm => vm.MedicationList);

			bindingSet.Bind(medicationList)
				.For(x => x.ItemClick)
				.To(vm => vm.Edit);


			bindingSet.Apply();
		}
	}
}