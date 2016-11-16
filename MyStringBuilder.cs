using System;
using System.Collections.Generic;
public class MyStringBuilder
{
	public char[] chars;
	public int length;
	public int capcity=10;
	public static void Main()
	{
		string str;
		str="hi";
		Console.WriteLine(str);
	}
	//无参构造
	public MyStringBuilder()
	{
		chars=new char[capcity];
		length=0;
	}
	//有参构造
	public MyStringBuilder(string other)
	{
		chars=new char[other.Length*2];
		length=other.Length*2;
		if(capcity<length)
			capcity=length;
	}
	//有参构造
	public MyStringBuilder(string other,int strMax)
	{
		chars=new char[other.Length*2];
		length=other.Length*2;
		capcity=strMax;
	}
	//////////////////添加越界了需要删了重新 申请
	public MyStringBuilder Append(string value)
	{
		if (value == NULL)
			return this;
		if (length + value.Length > capcity)
		{
			while (length + value.Length > capcity) 
			{
				capcity *= 2 ;
			}
			char[] temp = new char[capcity];
			temp=string.Copy (chars);
			chars = new char[capcity];
			chars=string.Copy (temp);
		}

		int index=chars.Length;
		for(int i=0;i<value.Length;i++ )
		{
			chars[index+i]=value[i];        //逐个添加
		}
		return this;
	}
	public MyStringBuilder Insert(int index, string value)
	{
		if (index < 0 || index > length || value == NULL)
			return this;
		if (length + value.Length > capcity)
		{
			while (length + value.Length > capcity) 
			{
				capcity *= 2 ;
			}
			char[] temp = new char[capcity];
			temp=string.Copy (chars);
			chars = new char[capcity];
			chars=string.Copy (temp);
		}
		int end=length-1;
		length=length+value.Length;
		//移动赋值
		for(int i=length;i>index+value.Length;i--)
			chars[i]=chars[end--];

		for(int i=index+value.Length;i>index;i--)
			chars[i]=value[i-index]; 
		return this;
	}
	public MyStringBuilder Replace(string oldValue, string newValue)
	{
		if (newValue == NULL || oldValue == NULL)
			return this;
		//将旧的字符串 替换成新的字符串 
		//从头开始查 有几个 相等的 
		List<int> m_list= new List<int>();
		for(int i=0;i<length;i++)
		{
			int bj = i;
			for(int j=0;j<length;j++)
			{
				if (chars [bj] == chars [j])
					break;
				if (j == length - 1)
				{
					m_list.Add (i);
					i = bj+1;
					break;
				}
				bj++;
			}
		}
		//统计有 多少个 需要替换的 string 
		//看不是需要扩容
		char[] temp ;
		length=length +  m_list.Count*(newValue.Length-oldValue.Length);
		if (length > capcty) 
		{
			while (length > capcity) 
			{
				capcity *= 2 ;
			}
			temp = new char[capcity];
			temp=string.Copy (chars);
			chars = new char[capcity];
			chars=string.Copy (temp);
		}

		int num = m_list.Count;
		int index = 0;
		int m_count = 0;
		int begin=0;				//用begin end 来控制正常加的范围
		int end=0;
		if(m_list[0]!=0 )
		{
			end = m_list [0];
		}
		for(int i=0;i<num ;i++)
		{
			m_count = 0;
			//正常加
			for (int j = begin; j < end; j++) {
				chars [index++] = temp [j];
				m_count++;
			}
				
			//加替换的那段
			for(int j=0;j<newValue.Length;j++)
				chars[index++]= temp [j];
			begin = begin + m_count + newValue.Length;				
			if(i + 1<num)
				end = m_list [i + 1];
		}
		//替换的加完了看还能否正常加
		if (index < length) 
		{
			begin = num;
			end = length;
			for (int j = begin; j < end; j++) 
				chars [index++] = temp [j];
		}
		//Ensure  X2
		return this;
	}
	public override string ToString()
	{
		//创建一个 string类型的 变量 然后返回
		string str=new string(chars);
		return str;
	}
}