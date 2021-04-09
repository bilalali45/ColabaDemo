import React, { useContext, useEffect, useState } from 'react'
import Notificaiton from '../../../Entities/Models/Notification';
import { NotificationActions } from '../../../Store/actions/NotificationActions';
import { NotificationActionsType } from '../../../Store/reducers/NotificationReducer';
import { Store } from '../../../Store/Store';
import ContentBody from '../../Shared/ContentBody';
import InfoDisplay from '../../Shared/InfoDisplay';
import Loader, {WidgetLoader} from '../../Shared/Loader';
import Table from '../../Shared/SettingTable/Table';
import TableROW from '../../Shared/SettingTable/TableROW';
import TableTD from '../../Shared/SettingTable/TableTD';
import TableTH from '../../Shared/SettingTable/TableTH';



const TimeIntervalEnum = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60]

export const NotificationBody = () => {

    const { state, dispatch } = useContext(Store);
    const notificationManager: any = state.notificationManager;
    const notificationData = notificationManager.notificationData;

    const  fetchNotificationSettings = async () => {
        let data = await NotificationActions.fetchNotificationSettings();
        dispatch({ type: NotificationActionsType.SetNotificationData, payload: data })
    }

    useEffect(() => {
        fetchNotificationSettings();
    }, []);


    const updateOptions = async (notificationTypeId: number, deliveryModeId: number, delayedInterval: number) => {

     let result = await NotificationActions.updateNotificationSettings(
         notificationTypeId, 
         deliveryModeId, 
         deliveryModeId === 2 ? delayedInterval : 0
         )
    }

    const handleSelect = (event: any, notificationItem: any, currentState: any) => {
        let updatedData = notificationData.map((d: any) => {
            if (event.target.checked && notificationItem.name === d.name) {
                d.changeState(currentState, notificationItem.interval);
                updateOptions(notificationItem.notificationTypeId, currentState, notificationItem.interval);
            }
            return d;
        });
        dispatch({ type: NotificationActionsType.SetNotificationData, payload: updatedData });    
    }

    const handleChange = (e: any) => {

     let notificationTypeId = parseInt(e.target.id);
     let value = parseInt(e.target.value);
     let updatedData = notificationData.map((d: any) => {
        if(notificationTypeId === d.notificationTypeId){
            d.changeState(d.currentState, value);
            updateOptions(notificationTypeId, d.currentState, value);
        }
        return d;
     });
     dispatch({ type: NotificationActionsType.SetNotificationData, payload: updatedData });
    }

    const renderDropdown = (n: any) => {
        return TimeIntervalEnum.map(d => {
            return (
                <option data-testid={`${n.name}-select-option`} selected={n.interval === d} value={d}>{d} min</option>
            )
        })
    }

    const rednerOptions = (n: any) => {
        return [3, 1, 2].map(d => {
            let name = d === 1 ? "Immediate" : d === 2 ? "Delayed" : "Off";
            return (
                <label className="label">
                    <input value={name} data-testid={`input-check-${d}`} onChange={(e) => handleSelect(e, n, d)} type="radio" checked={n?.currentState === d} name={n.name + '-' + d} id="" /> {name}
                </label>
            )
        })
    }

    const renderRows = () => {
        return notificationData?.map((d:Notificaiton) => {
            return (
                <TableROW>
                    <TableTD><span className={d.currentState != 3 ? "notOff settings__table--seprate-right" : "settings__table--seprate-right"}>{d?.name}</span></TableTD>
                    <TableTD>
                        {rednerOptions(d)}
                        <span>
                            {d.showTime &&
                                <select data-testid={`input-select`} id={ String(d.notificationTypeId)} onChange={handleChange} >
                                    {renderDropdown(d)}
                                </select>
                            }
                        </span>
                    </TableTD>
                    <TableTD valign="middle" align="right">
                        <InfoDisplay tooltipType={d.notificationTypeId}/>
                    </TableTD>
                </TableROW>
            )
        })
    }

  if(!notificationData)
  {
    return <WidgetLoader reduceHeight="52px"/>
  }

    return (
        <div data-testid="NotificationBody">
        <ContentBody className="notification-body">
            <Table tableClass="notification-table">
                <TableROW >
                    <TableTH>Type</TableTH>
                    <TableTH>Set your preferences</TableTH>
                </TableROW>
                {renderRows()}
            </Table>
        </ContentBody>
        </div>
    )
}
