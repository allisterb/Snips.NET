﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SnipsNLU
{
    /// <summary>
    /// Enum representing the grain of a resolved date related value
    /// </summary>
    public enum SNIPS_GRAIN
    {
        /*
         * The resolved value has a granularity of a year
         */
        SNIPS_GRAIN_YEAR = 0,
        /*
         * The resolved value has a granularity of a quarter
         */
        SNIPS_GRAIN_QUARTER = 1,
        /*
         * The resolved value has a granularity of a mount
         */
        SNIPS_GRAIN_MONTH = 2,
        /*
         * The resolved value has a granularity of a week
         */
        SNIPS_GRAIN_WEEK = 3,
        /*
         * The resolved value has a granularity of a day
         */
        SNIPS_GRAIN_DAY = 4,
        /*
         * The resolved value has a granularity of an hour
         */
        SNIPS_GRAIN_HOUR = 5,
        /*
         * The resolved value has a granularity of a minute
         */
        SNIPS_GRAIN_MINUTE = 6,
        /*
         * The resolved value has a granularity of a second
         */
        SNIPS_GRAIN_SECOND = 7,
    }

    public enum SNIPS_PRECISION
    {
        /*
         * The resolved value is approximate
         */
        SNIPS_PRECISION_APPROXIMATE = 0,
        /*
         * The resolved value is exact
         */
        SNIPS_PRECISION_EXACT = 1,
    }

    /// <summary>
    /// Used as a return type of functions that can encounter errors
    /// </summary>
    public enum SNIPS_RESULT {
        /*
         * The function returned successfully
         */
        SNIPS_RESULT_OK = 0,
        /*
         * The function encountered an error, you can retrieve it using the dedicated function
         */
        SNIPS_RESULT_KO = 1,
    }

    /// <summary>
    /// Enum type describing how to cast the value of a CSlotValue
    /// </summary>
    public enum SNIPS_SLOT_VALUE_TYPE
    {
        /*
         * Custom type represented by a char *
         */
        SNIPS_SLOT_VALUE_TYPE_CUSTOM = 1,
        /*
         * Number type represented by a CNumberValue
         */
        SNIPS_SLOT_VALUE_TYPE_NUMBER = 2,
        /*
         * Ordinal type represented by a COrdinalValue
         */
        SNIPS_SLOT_VALUE_TYPE_ORDINAL = 3,
        /*
         * Instant type represented by a CInstantTimeValue
         */
        SNIPS_SLOT_VALUE_TYPE_INSTANTTIME = 4,
        /*
         * Interval type represented by a CTimeIntervalValue
         */
        SNIPS_SLOT_VALUE_TYPE_TIMEINTERVAL = 5,
        /*
         * Amount of money type represented by a CAmountOfMoneyValue
         */
        SNIPS_SLOT_VALUE_TYPE_AMOUNTOFMONEY = 6,
        /*
         * Temperature type represented by a CTemperatureValue
         */
        SNIPS_SLOT_VALUE_TYPE_TEMPERATURE = 7,
        /*
         * Duration type represented by a CDurationValue
         */
        SNIPS_SLOT_VALUE_TYPE_DURATION = 8,
        /*
         * Percentage type represented by a CPercentageValue
         */
        SNIPS_SLOT_VALUE_TYPE_PERCENTAGE = 9,
        /*
         * Music Album type represented by a char *
         */
        SNIPS_SLOT_VALUE_TYPE_MUSICALBUM = 10,
        /*
         * Music Artist type represented by a char *
         */
        SNIPS_SLOT_VALUE_TYPE_MUSICARTIST = 11,
        /*
         * Music Track type represented by a char *
         */
        SNIPS_SLOT_VALUE_TYPE_MUSICTRACK = 12,
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class ffi
    {
        /// <summary>
        /// API version we are targeting
        /// </summary>
        static readonly string SNIPS_NLU_VERSION = "0.62.0-SNAPSHOT";

        /// <summary>
        /// Results of the intent classifier
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CIntentClassifierResult
        {
            /*
             * Name of the intent detected
             */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string intent_name;
        
            /*
             * Between 0 and 1
             */
            readonly float probability;
        }

        /// <summary>
        /// Struct describing a Slot
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CSlotValue
        {
            /*
             * Points to either a *const char, a CNumberValue, a COrdinalValue,
             * a CInstantTimeValue, a CTimeIntervalValue, a CAmountOfMoneyValue,
             * a CTemperatureValue or a CDurationValue depending on value_type
             */
            readonly IntPtr value;

           /*
            * The type of the value
            */
            readonly SNIPS_SLOT_VALUE_TYPE value_type;
        }


        /// <summary>
        /// Struct describing a Slot
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CSlot
        {
           /*
            * The resolved value of the slot
            */
            readonly CSlotValue value;

            /*    
             * The raw value as it appears in the input text
             */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string raw_value;

            /*
            * Name of the entity type of the slot
            */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string entity;
            
            /*
            * Name of the slot
            */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string slot_name;
            
            /*
            * Start index of raw value in input text
            */
            readonly int range_start;

           /*
            * End index of raw value in input text
            */
            readonly int range_end;
        }

        /*
        * Wrapper around a slot list
        */
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CSlotList
        {
           /*
            * Pointer to the first slot of the list
            */
            [MarshalAs(UnmanagedType.LPArray)]
            readonly CSlot[] slots;
           
           /*
            * Number of slots in the list
            */
            readonly int size;
        }


        /// <summary>
        /// Results of intent parsing
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CIntentParserResult
        {

            /*
             * The text that was parsed
             */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string input;

            /*
             * The result of intent classification, may be null if no intent was detected
             */
             [MarshalAs(UnmanagedType.LPStruct)]
             readonly CIntentClassifierResult intent;

            /*
             * The slots extracted if an intent was detected
             */
             [MarshalAs(UnmanagedType.LPArray)]
             readonly CSlotList[] slots;
        }


        /// <summary>
        /// Representation of an instant value
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CInstantTimeValue
        {
            /*
            * String representation of the instant
            */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string value;
            
            /*
             * The grain of the resolved instant
             */
            readonly SNIPS_GRAIN grain;

            /*
             * The precision of the resolved instant
             */
            readonly SNIPS_PRECISION precision;
        }


        /// <summary>
        /// Representation of an interval value
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CTimeIntervalValue
        {
            /*
             * String representation of the beginning of the interval
             */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string from;

            /*
             * String representation of the end of the interval
             */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string to;
        }


        /// <summary>
        /// Representation of an amount of money value
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CAmountOfMoneyValue
        {
            /*
            * The currency
            */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string unit;
        
            /*
             * The amount of money
             */
            readonly float value;
        
            /*
             * The precision of the resolved value
             */
            readonly SNIPS_PRECISION precision;
        }


        /// <summary>
        /// Representation of a temperature value
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CTemperatureValue
        {
            /*
             * The unit used
             */
            [MarshalAs(UnmanagedType.LPStr)]
            readonly string unit;

        
            /*
             * The temperature resolved
             */
            readonly float value;
        }

        /// <summary>
        /// Representation of a duration value
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        readonly struct CDurationValue
        {
            /*
             * Number of years in the duration
             */
            readonly long years;
            /*
             * Number of quarters in the duration
             */
                readonly long quarters;
            /*
             * Number of months in the duration
             */
            readonly long months;
            /*
             * Number of weeks in the duration
             */
            readonly long weeks;
            /*
             * Number of days in the duration
             */
            readonly long days;
            /*
             * Number of hours in the duration
             */
            readonly long hours;
            /*
             * Number of minutes in the duration
             */
            readonly long minutes;
            /*
             * Number of seconds in the duration
             */
            readonly long seconds;
            /*
             * Precision of the resolved value
             */
            readonly SNIPS_PRECISION precision;
        }

        [DllImport("snips_nlu_ffi", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe SNIPS_RESULT snips_nlu_engine_create_from_dir
            ([In, MarshalAs(UnmanagedType.LPStr)] string root_dir, [In, Out] ref IntPtr client);

        [DllImport("snips_nlu_ffi", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe SNIPS_RESULT snips_nlu_engine_get_model_version([In, Out] ref IntPtr version);

    }
}
