using System;
using System.Windows.Forms;
using FractalPainting.Infrastructure;

namespace FractalPainting.App
{
	public class SettingsManager
	{
		private readonly IObjectSerializer serializer;
		private readonly IBlobStorage storage;
		private string settingsFilename;

		public SettingsManager(IObjectSerializer serializer, IBlobStorage storage)
		{
			this.serializer = serializer;
			this.storage = storage;
		}

		public AppSettings Load()
		{
			try
			{
				settingsFilename = "app.settings";
				byte[] data = storage.Get(settingsFilename);
				if (data == null)
				{
					var defaultSettings = CreateDefaultSettings();
					Save(defaultSettings);
					return defaultSettings;
				}
				return serializer.Deserialize<AppSettings>(data);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "�� ������� ��������� ���������");
				return CreateDefaultSettings();
			}
		}

		private static AppSettings CreateDefaultSettings()
		{
			return new AppSettings
			{
				ImagePath = ".",
				ImageSettings = new ImageSettings(),
				MainWindowState = FormWindowState.Normal
			};
		}

		public void Save(AppSettings settings)
		{
			storage.Set(settingsFilename, serializer.Serialize(settings));
		}

	}
}