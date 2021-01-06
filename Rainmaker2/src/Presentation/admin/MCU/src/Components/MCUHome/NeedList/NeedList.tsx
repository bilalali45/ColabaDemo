import React, { useContext, useEffect, useState } from 'react'
import { NeedListActions } from '../../../Store/actions/NeedListActions'
import { NeedListActionsType } from '../../../Store/reducers/NeedListReducer'
import { Store } from '../../../Store/Store'
import { LocalDB } from '../../../Utils/LocalDB'
import { LoanSnapshot } from './LoanSnapshot/LoanSnapshot'
import { Navigation } from './Navigation/Navigation'
import { NeedListHeader } from './NeedListHeader/NeedListHeader'
import { NeedListView } from './NeedListView/NeedListView'
import { NeedListLockIcon } from "../../../Shared/SVG";
import Modal from "react-bootstrap/Modal";
import errorIcon from "../../../Assets/images/error-icon.svg";
import Button from "react-bootstrap/Button";

let timer: any;

export const NeedList = () => {

    const [acquireLockFailed, setAquireLockFailed] = useState<boolean>(false);

    const { state, dispatch } = useContext(Store);

    const needListManager: any = state?.needListManager;
    const isNeedListLocked = needListManager?.isNeedListLocked;


    useEffect(() => {
        if (!isNeedListLocked?.lockUserName) {
            aquireLock();
        }
        if (isNeedListLocked?.lockUserId === +LocalDB.getUserPayload()?.UserProfileId) {
            timer = setInterval(() => {
                retainLock();
            }, 1000 * window?.envConfig?.LOCK_RETAIN_DURATION);
        }
        return () => {
            clearInterval(timer);
            console.log('in here clear interval');
        }
    }, [isNeedListLocked?.lockUserId]);

    const aquireLock = async () => {
        let res = await NeedListActions.aquireLock();
        if(res.status === 200 || (res?.status === 400 && res?.data?.id)) {
            dispatch({ type: NeedListActionsType.SetIsNeedListLocked, payload: res.data });
        }else {
            setAquireLockFailed(true);
        }


    }

    const retainLock = async () => {
        let lockRetained = await NeedListActions.retainLock();
        dispatch({ type: NeedListActionsType.SetIsNeedListLocked, payload: lockRetained })
    }

    if(acquireLockFailed) {
        return <h1>Need List Failure</h1>
    }

    if (!isNeedListLocked) {
        return <div></div>
    }

    const redirectToMVC = () => {
        const portalReferralUrl = LocalDB.getPortalReferralUrl();
    
        if (portalReferralUrl) {
          window.top.location.href = portalReferralUrl;
        } else {
          window.top.location.href = '/Admin/Dashboard';
        }
      };

    const renderNeedListLock = () => {
        return (
            <Modal
                show={checkIsLocked()}
                backdrop="static"
                keyboard={true}
                className="modal-alert need-list-lock"
                centered
            >
                <Modal.Header closeButton>

                </Modal.Header>
                <Modal.Body>
                    <div>
                        <figure><NeedListLockIcon /></figure>
                        <h4>This Need List is Locked By: <span>{isNeedListLocked?.lockUserName?.toUpperCase()}</span></h4>
                        <Button variant="primary" onClick={redirectToMVC}>{"Go Back"}</Button>
                    </div>
                </Modal.Body>
            </Modal>
        )
    }
    console.log('isNeedListLocked?.lockUserId', isNeedListLocked?.lockUserId)
    console.log('LocalDB.getUserPayload()?.UserProfileId', LocalDB.getUserPayload()?.UserProfileId)
    const checkIsLocked = () => isNeedListLocked?.lockUserId !== Number(LocalDB.getUserPayload()?.UserProfileId);

    return (
        <div className={checkIsLocked() ? 'needListlocked' : ''}>
            <LoanSnapshot />
            {/* <Navigation/> */}
            <NeedListHeader />

            <div className="container-mcu">
                <div className="block">
                    <NeedListView />
                </div>
            </div>
            {checkIsLocked() ? renderNeedListLock() : ''}
        </div>
    )
}
