using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSVHelper;
[System.Serializable]
public class TanGeJinBiConfig {
	public 	int	id;
	public 	string	title;
	public 	string	spriteName;
	public 	string	content;
	public 	int	type;
	 public static Dictionary<int,TanGeJinBiConfig>  dic = new  Dictionary<int,TanGeJinBiConfig>();
	public static void load(){
		CsvHelper.ParseCSV("TanGeJinBiConfig",OnLoaded,true);
	}
	public static void OnLoaded(List<CsvRow> rows){
		dic.Clear();
		for(int i =3;i < rows.Count;i++){
		string[] values = rows[i].ToArray();
		 if(values.Length<=0) continue;
		TanGeJinBiConfig	 elem = new TanGeJinBiConfig();
		if(values.Length >0)
		{
			if(!string.IsNullOrEmpty(values[0])){
				int.TryParse(values[0],out elem.id);
			}
		}
		if(values.Length >1)
		{
				elem.title= values[1];
		}
		if(values.Length >2)
		{
				elem.spriteName= values[2];
		}
		if(values.Length >3)
		{
				elem.content= values[3];
		}
		if(values.Length >4)
		{
			if(!string.IsNullOrEmpty(values[4])){
				int.TryParse(values[4],out elem.type);
			}
		}
		TanGeJinBiConfig.dic[elem.id] = elem;
		}
	}
}
