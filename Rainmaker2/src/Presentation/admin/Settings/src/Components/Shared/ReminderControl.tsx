import { isEmpty } from 'lodash';
import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import { DropDown } from './DropDown';
import { Toggler } from './Toggler';


interface ReminderControlProps {
    number: string;
    days?: string;
    time?: string;
    timeType?: string;
    makeEnabled?: boolean;
    handlerDays: Function;
    handlerTime: Function;
    handlerTimeType: Function;
    handlerEnabled: Function;
    handlerDelete: Function;
    handlerInput: Function;
}


const ReminderControl: React.FC<ReminderControlProps> = ({ number, days, time, timeType, makeEnabled, handlerDays, handlerTime, handlerTimeType, handlerEnabled, handlerDelete, handlerInput
}) => {

    const [showReminderDropdown, setShowReminderDropdown] = useState(false);
    const dropdown = useRef<HTMLDivElement>(null);
    const dropdownText = useRef<HTMLDivElement>(null);
    const refShowOption = useRef<any>(null);

    const [data, setstate] = useState({
        days: days ? days : '00',
        time: time ? time : '00:00',
        timeType: timeType ? timeType : 'PM'
    });

    //set position mega dropdown
    const [getPositionDropDown, setPositionDropDown] = useState({ x: 0, y: 0 });
    const styling: any = { position: 'fixed', left: getPositionDropDown.x, top: getPositionDropDown.y };

    //Set position menu dropdown
    const [getPositionMenuDown, setPositionMenuDown] = useState({ x: 0, y: 0 });
    const stylingMenuDown: any = { position: 'fixed', left: (getPositionMenuDown.x - 175), top: getPositionMenuDown.y };
    

    const toggleDropDown = async () => {
        setPositionDropDown({
            x: Number(dropdownText.current?.getBoundingClientRect().x),
            y: Number(dropdownText.current?.getBoundingClientRect().y) + 44
        });
        await setShowReminderDropdown(!showReminderDropdown);
        setShowOption(false);
    }

    // State For Menu Toggle
    const [showOption, setShowOption] = useState(false);
    const toggleShowOption = async () => {
        setPositionMenuDown({
            x: Number(refShowOption.current?.getBoundingClientRect().x),
            y: Number(refShowOption.current?.getBoundingClientRect().y) + 44
        });
        await setShowOption(!showOption);
        setShowReminderDropdown(false);    
        
        console.log('Menu : ',getPositionMenuDown, refShowOption.current?.getBoundingClientRect())
    }

    // State For Days
    const [stateDays, setstateDays] = useState([
        { text: '02', value: '02' },
        { text: '04', value: '04' },
        { text: '06', value: '06' },
        { text: '08', value: '08' },
        { text: '10', value: '10' }
    ]);   
    
    // Selected Day State
    const [selectedDay, setSelectedDay] = useState<[{text:string,value:string}]>();

    // transfer data on click - child to parent
    const selectDay = (selectDay: any) => {
        handlerDays(selectDay);
    }

    //let currentChangeDay = '';
    const changeDay = (ele:any) => {
        handlerInput(ele);
        setSelectedDay(ele);
    }

    // set selected day
    let abc; 
    const checkTrueDay =  stateDays.map((item:any)=>{
        if(selectedDay == undefined && (item.value != days)){
            abc = {text: String(days), value:String(days)};
            
        }else{
            abc = {text: String(days), value:String(days)};
        } 
        return abc;
    });

    // State For Time
    const [stateTime, setstateTime] = useState([        
        { text: '12:00', value: '12:00' },
        { text: '12:30', value: '12:30' },     
        { text: '01:00', value: '01:00' },
        { text: '01:30', value: '01:30' },
        { text: '02:00', value: '02:00' },
        { text: '02:30', value: '02:30' },
        { text: '03:00', value: '03:00' },
        { text: '03:30', value: '03:30' },
        { text: '04:00', value: '04:00' },
        { text: '04:30', value: '04:30' },
        { text: '05:00', value: '05:00' },
        { text: '05:30', value: '05:30' },
        { text: '06:00', value: '06:00' },
        { text: '06:30', value: '06:30' },
        { text: '07:00', value: '07:00' },
        { text: '07:30', value: '07:30' },
        { text: '08:00', value: '08:00' },
        { text: '08:30', value: '08:30' },
        { text: '09:00', value: '09:00' },
        { text: '09:30', value: '09:30' },
        { text: '10:00', value: '10:00' },
        { text: '10:30', value: '10:30' },
        { text: '11:00', value: '11:00' },
        { text: '11:30', value: '11:30' }
    ]);

    // transfer data on click - child to parent
    const selectTime = (selectTime: any) => {
        handlerTime(selectTime);
    }

    // set selected day
    const checkTrueTime =  stateTime.filter((item:any)=>{
        return item.value == time;
    });

    // Type Conversion
    const checkTimeType = () => {
        return (timeType == "AM" ? false : true)
    }

    // Click Outside
    useEffect(() => { 
        const clickOutside = (e: any) => {
            if (!dropdown.current?.contains(e.target)) {
                setShowReminderDropdown(false);

            }
        }
        document.addEventListener('click', clickOutside);

        const clickOutSideMenu = (e: any) => {
            if (!refShowOption.current?.contains(e.target)) {
                setShowOption(false);
            }
        }
        document.addEventListener('click', clickOutSideMenu);

        const updateDropDown = ()=>{
            setShowReminderDropdown(false);
            setShowOption(false);
        }
        window.addEventListener('resize', updateDropDown);

        return () => {
            document.removeEventListener('click', clickOutside);
            document.removeEventListener('click', clickOutSideMenu);
            window.removeEventListener('resize', updateDropDown);
        }
    }, [data]);

    const filterDay = () =>{
        
        if(String(days).length == 1 && days=='0')
        {
            return '0';
        }
        else if(String(days).length == 1 || isEmpty(String(days)))
        {
            return "0"+String(days);
        }
        else{
            return String(days);
        }
    }
    
    return (
        <div data-testid="reminderControl" className={`settings__reminder-control ${makeEnabled ? '' : 'disabled' }`} ref={dropdown}>
            <div className="settings__reminder-control-wrap">
                <span className="settings__reminder-control-count" onClick={toggleDropDown}>{number}</span>

                <div data-testid="toggle-drpdwn-btn" className="settings__reminder-control-dropdown" onClick={toggleDropDown} ref={dropdownText}>
                    <span className="settings__reminder-control-dropdown--text" title={`Send email ${days} Days after request at ${time} ${timeType}`} >Send email <span>{filterDay()}</span> Days after request at <span>{time} {timeType}</span></span>
                    <span className="settings__reminder-control-dropdown--btn"><i className="zmdi zmdi-chevron-down"></i></span>
                </div>

                <button className="settings__reminder-control-btn settings__dropdownlist arrow-right" ref={refShowOption}>
                    <div data-testid="reminderControlBtn" className="settings__reminder-control-btn-wrap" onClick={toggleShowOption}>
                        <i className="zmdi zmdi-more-vert"></i>
                    </div>
                    {showOption &&
                        <div data-testid="reminderControlBtnDropDown" className="settings__dropdownlist-menu" style={stylingMenuDown}>
                            <div className="settings__dropdownlist-wrap">
                                <ul>
                                    <li>
                                        <button className="button-no-style flex flex-space-between" onClick={() =>{ setTimeout(() => { toggleShowOption() }, 400); handlerEnabled(); }}>
                                            <span className="d-text">Disable/Enable</span>
                                            <Toggler checked={makeEnabled} handlerClick={() =>{}} />
                                        </button>
                                    </li>
                                    <li>
                                        <button className="button-no-style flex flex-space-between" onClick={() => { setTimeout(() => { toggleShowOption() }, 400); handlerDelete(); }}>
                                            <span className="d-text">Delete</span>
                                            <span className="d-icon"><em className="zmdi zmdi-close"></em></span>
                                        </button>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    }
                </button>
            </div>

            {showReminderDropdown &&
                <div data-testid="item-control" className="settings__reminder-control-dropdown-menu" style={styling}>
                    <div className="settings__reminder-control-dropdown-menu-wrap">

                        <label>Send email</label>
                        <DropDown 
                            editable={true} 
                            listData={stateDays} 
                            selectedValue={checkTrueDay} 
                            handlerSelect={(ele: any) => { selectDay(ele) }} 
                            handlerInput={(ele:any) => { changeDay(ele)  }}
                            maxLength={2}
                            inputType={'text'}
                        />

                        <label> Days after request at</label>
                        <DropDown 
                            listData={stateTime} 
                            selectedValue={checkTrueTime} 
                            handlerSelect={(ele: any) => { selectTime(ele) }} 
                        />

                        <Toggler checked={checkTimeType()} handlerClick={() => handlerTimeType()} trueValue="AM" falseValue="PM" />

                    </div>
                </div>
            }



        </div>
    )
}

export default ReminderControl;



