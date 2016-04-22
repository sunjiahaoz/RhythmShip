using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSVHelper;
[System.Serializable]
public class Equip_FirePointConfig {
	public 	int	id;
	public 	string	prefabName;
	 public static Dictionary<int,Equip_FirePointConfig>  dic = new  Dictionary<int,Equip_FirePointConfig>();
	public static void load(){
		CsvHelper.ParseCSV("Equip_FirePointConfig",OnLoaded,true);
	}
	public static void OnLoaded(List<CsvRow> rows){
		dic.Clear();
		for(int i =3;i < rows.Count;i++){
		string[] values = rows[i].ToArray();
		 if(values.Length<=0) continue;
		Equip_FirePointConfig	 elem = new Equip_FirePointConfig();
		if(values.Length >0)
		{
			if(!string.IsNullOrEmpty(values[0])){
				int.TryParse(values[0],out elem.id);
			}
		}
		if(values.Length >1)
		{
				elem.prefabName= values[1];
		}
		Equip_FirePointConfig.dic[elem.id] = elem;
		}
	}
}
