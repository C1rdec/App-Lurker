﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Caliburn.Micro;
using AppLurker.Models;
using AppLurker.Services;
using AppLurker.ViewModels;

namespace AppLurker
{
    public class AppBootstrapper : BootstrapperBase
    {
        #region Fields

        private SimpleContainer _container;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper"/> class.
        /// </summary>
        public AppBootstrapper()
        {
            Initialize();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Singleton<SettingsService, SettingsService>();
            _container.Singleton<FlyoutService, FlyoutService>();
            _container.Singleton<ThemeService, ThemeService>();
            _container.Singleton<SettingsViewModel, SettingsViewModel>();
            _container.Singleton<DashboardViewModel, DashboardViewModel>();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<ConfigurationService, ConfigurationService>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<ShellViewModel, ShellViewModel>();
            _container.Instance(Application);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        /// The located service.
        /// </returns>
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>
        /// The located services.
        /// </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        #endregion
    }
}