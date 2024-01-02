using System;
using System.IO;
using System.Text;
using UnityEngine;

public class FileDataHandler
{
	private string directoryPath = "";
	private string filename = "";

	private bool isEncrypt;
	private bool isBase64;

	private CryptoModule cryptoModule;

	public FileDataHandler(string directoryPath, string fiilname, bool isEncrypt, bool isBase64)
	{
		this.directoryPath = directoryPath;
		this.filename = fiilname;
		this.isEncrypt = isEncrypt;
		this.isBase64 = isBase64;

		cryptoModule = new CryptoModule();
	}

	public void Save(GameData gameData)
	{
		string fullPath = Path.Combine(directoryPath, filename);
		try
		{
			Directory.CreateDirectory(directoryPath);
			string dataToStore = JsonUtility.ToJson(gameData, true); // 인간이 보기 좋게

			if (isEncrypt)
			{
				dataToStore = cryptoModule.AESEncrypt256(dataToStore);
				//dataToStore = EncryptDecryptData(dataToStore);
			}

			/*if (isBase64)
			{
				dataToStore = Base64Process(dataToStore, true);
			}*/

			using (FileStream writeStream = new FileStream(fullPath, FileMode.Create))
			{
				using (StreamWriter writer = new StreamWriter(writeStream))
				{
					writer.Write(dataToStore);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError($"Error on trying to save data to file {fullPath}");
		}
	}

	public GameData Load()
	{
		string fullPath = Path.Combine(directoryPath, filename);
		GameData loadedData = null;

		if (File.Exists(fullPath))
		{
			try
			{
				string dataToLoad = "";
				using (FileStream readStream = new FileStream(fullPath, FileMode.Open))
				{
					using (StreamReader reader = new StreamReader(readStream))
					{
						dataToLoad = reader.ReadToEnd(); // 끝까지 다 읽기
					}
				}

				/*if (isBase64)
				{
					dataToLoad = Base64Process(dataToLoad, false);
				}*/

				if (isEncrypt)
				{
					dataToLoad = cryptoModule.Decrypt(dataToLoad);
					//dataToLoad = EncryptDecryptData(dataToLoad);
				}

				loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error on trying to load data from file {fullPath}");
			}
		}

		return loadedData;
	}

	public void DeleteSaveData()
	{
		string fullPath = Path.Combine(directoryPath, filename);

		if (File.Exists(fullPath))
		{
			try
			{
				File.Delete(fullPath);
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error on trying to load data from file {fullPath}");
			}
		}

	}

	private string codeWord = "ggmisgreate";
	private string EncryptDecryptData(string data)
	{
		StringBuilder builder = new StringBuilder();

		for (int i = 0; i < data.Length; i++)
		{
			builder.Append((char)(data[i] ^ codeWord[i % codeWord.Length]));
		}

		return builder.ToString();
	}

	private string Base64Process(string data, bool encoding)
	{
		if (encoding)
		{
			byte[] dataByteArr = Encoding.UTF8.GetBytes(data);
			return Convert.ToBase64String(dataByteArr); // 바이트를 싹 끄집어와서 6비트로 쪼개서 스트링으로 조립 
		}
		else
		{
			byte[] dataByteArr = Convert.FromBase64String(data);
			return Encoding.UTF8.GetString(dataByteArr);
		}
	}
}