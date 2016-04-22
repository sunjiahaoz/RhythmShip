using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSVHelper;
[System.Serializable]
public class Equip_SkillConfig {
	public 	int	id;
	public 	string	prefabName;
	 public static Dictionary<int,Equip_SkillConfig>  dic = new  Dictionary<int,Equip_SkillConfig>();
	public static void load(){
		CsvHelper.ParseCSV("Equip_SkillConfig",OnLoaded,true);
	}
	public static void OnLoaded(List<CsvRow> rows){
		dic.Clear();
		for(int i =3;i < rows.Count;i++){
		string[] values = rows[i].ToArray();
		 if(values.Length<=0) continue;
		Equip_SkillConfig	 elem = new Equip_SkillConfig();
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
		Equip_SkillConfig.dic[elem.id] = elem;
		}
	}
}
