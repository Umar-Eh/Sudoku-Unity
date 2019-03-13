using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System.Threading.Tasks;


	public class Program : MonoBehaviour
    {
        

        static int[,] rows = { { 0, 1 ,2}, { 0, 1, 2}, { 0, 1, 2 }, { 3, 4, 5 }, { 3, 4, 5 }, { 3, 4, 5 }, { 6, 7, 8 }, { 6, 7, 8 }, { 6, 7, 8 } };
        static int[,] columns = { { 0, 3, 6}, { 1, 4, 7}, { 2, 5, 8}, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 } , { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 } };



        public static bool Checker(int square, int row, int column)
        {
            if(CheckSquare(square,row,column) && CheckRow(square, row, column) && CheckColumn(square, row, column))
            {
                return true;
            }

            return false; 
            
        }

        public static bool CheckRow(int square, int row, int column)
        {
            for(int index = 0; index < 3; index++)
            {
                for(int element = 0; element < 3; element++){
                    if(rows[square,index] != square || element != column)
                    {
						if(GameManager.grid[rows[square, index], row,element].txtvalue.text == GameManager.grid[square, row, column].txtvalue.text)
                        {
                            return false;
                        }

                    }

                }
            }
            return true;

        }

        public static bool CheckColumn(int square, int row, int column)
        {
            for(int index = 0; index < 3; index++)
            {
                for(int element = 0; element < 3; element++)
                {
                    if(columns[square,index] != square || element != row)
                    {
                        
					if (GameManager.grid[columns[square, index], element,column].txtvalue.text == GameManager.grid[square, row,column].txtvalue.text)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static bool CheckSquare(int square, int row, int column)
        {
            for(int index = 0; index < 3; index++)
            {
                for (int element = 0; element < 3; element++)
                {
                    if(index != row || element != column)
                    {
					if(GameManager.grid[square, row, column].txtvalue.text == GameManager.grid[square, index, element].txtvalue.text)
                        {
                            return false; 
                        }
                    }
                }
            }

            return true; 
        }
    }

