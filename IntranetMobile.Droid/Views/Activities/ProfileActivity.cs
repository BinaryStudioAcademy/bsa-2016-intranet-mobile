using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using IntranetMobile.Core.ViewModels.Profile;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Theme = "@style/BSTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class ProfileActivity : BaseToolbarActivity<ProfileViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_profile;

        public override int ToolbarLayout { get; } = Resource.Id.activity_profile_toolbar;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var technologyCategoriesRecyclerView =
                FindViewById<MvxRecyclerView>(Resource.Id.content_profile_technology_categories_recyclerview);

            technologyCategoriesRecyclerView.ItemTemplateSelector = new TypeTemplateSelector();

            // TODO: Not working for now
            //technologyCategoriesRecyclerView.HasFixedSize = true;
            //technologyCategoriesRecyclerView.Adapter = new TechnologyCategoriesAdapter((IMvxAndroidBindingContext)BindingContext);

            var bindingSet = this.CreateBindingSet<ProfileActivity, ProfileViewModel>();
            bindingSet.Bind(SupportActionBar)
                .For(bar => bar.Title)
                .To(vm => vm.FullName);
            bindingSet.Apply();
        }
    }

    public class TypeTemplateSelector : IMvxTemplateSelector
    {
        private readonly Dictionary<Type, int> _typeMapping;

        public TypeTemplateSelector()
        {
            _typeMapping = new Dictionary<Type, int>
            {
                {typeof(UserTechnologyCategoryViewModel), Resource.Layout.item_technology_category},
                {typeof(UserTechnologyViewModel), Resource.Layout.item_technology}
            };
        }

        public int GetItemViewType(object forItemObject)
        {
            return _typeMapping[forItemObject.GetType()];
        }

        public int GetItemLayoutId(int fromViewType)
        {
            return fromViewType;
        }
    }

    // TODO: Not working for now
    public class TechnologyCategoriesAdapter : MvxRecyclerAdapter
    {
        public TechnologyCategoriesAdapter(IMvxAndroidBindingContext bindingContext) : base(bindingContext)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);

            MvxRecyclerView recyclerView = null;
            // holder.ItemView.FindViewById<MvxRecyclerView>(Resource.Id.content_profile_technologies_recyclerview);

            recyclerView.HasFixedSize = true;
            recyclerView.NestedScrollingEnabled = false;
            var layoutManager = new LinearLayoutManager(recyclerView.Context);
            recyclerView.SetLayoutManager(layoutManager);
            layoutManager.AutoMeasureEnabled = true;
        }
    }
}