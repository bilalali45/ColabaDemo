import React, { FunctionComponent, useState, useContext, useEffect } from 'react';
import { Link, useHistory, useLocation } from 'react-router-dom';
import Accordion from 'react-bootstrap/Accordion';
import { useAccordionToggle } from 'react-bootstrap/AccordionToggle';
import { LocalDB } from '../../Utils/LocalDB';
import { navigation, Role } from '../../Store/Navigation';
import { SVGUser, SVGTodoList, SVGUsers, SVGManageUsers, SVGTeamRoles, SVGNeedList, SVGDocumentTemplates, SVGRequestEmailTemplates, SVGIntegrations, SVGLoanOriginationSystem, SVGNeedListReminderEmails, SVGLoanStatusUpdate } from './SVG';
import { AlertBox } from './AlertBox';
import { Store } from '../../Store/Store';
import { RequestEmailTemplateActionsType } from '../../Store/reducers/RequestEmailTemplateReducer';

interface SideNav { }

const ContextAwareToggle = ({ className, children, eventKey, callback }: any) => {
  const decoratedOnClick = useAccordionToggle(
    eventKey,
    () => callback && callback(eventKey)
  );

  

  return (
    <button
      data-testid="settings__accordion-header"
      type="button"
      onClick={decoratedOnClick}
      className={`settings__accordion-header ${className ? className : ''}`}
    >
      {children}
    </button>
  );
}

export const SideBar: FunctionComponent<SideNav> = ({ props }: any) => {
  const filteredNavigation = navigation.filter((nav) => nav.role === LocalDB.getUserRole());
  let path = LocalDB.getCurrentUrl().split('/');
  
  const {state, dispatch} = useContext(Store);
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const showAlertBox = emailTemplateManger.showAlertBox;
  const isFieldEdited = emailTemplateManger.editedFields;
  const [activeNavAdmin, setActiveNavAdmin] = useState<any>(0);  // set by index
  const [activeNavMCU, setActiveNavMCU] = useState('/' + path[1]);
  const [activeSubNav, setActiveSubNav] = useState('/' + path[1]); // Set by Link Name
  const [showAlert, setshowAlert] = useState<boolean>(false);
  const [currentNav , setCurrentNav] = useState('');

  const setIcon = (ele: any) => {
    switch (ele) {
      case 'Profile':
        return (<SVGUser />);
      case 'Users':
        return (<SVGUsers />);
      case 'DocumentTemplates':
        return (<SVGTodoList />);
      case 'ManageUsers':
        return (<SVGManageUsers />);
      case 'TeamRoles':
        return (<SVGTeamRoles />);
      case 'NeedsList':
        return (<SVGNeedList />);
      case 'DocumentTemplates':
        return (<SVGDocumentTemplates />);
      case 'RequestEmailTemplates':
        return (<SVGRequestEmailTemplates />);
      case 'LoanOriginationSystem':
        return (<SVGLoanOriginationSystem />);
      case 'Integrations':
        return (<SVGIntegrations />);
      case 'NeedsListReminderEmails':
        return (<SVGNeedListReminderEmails/>);
      case 'LoanStatusUpdate':
        return (<SVGLoanStatusUpdate/>);
      default:
        return (<SVGUser />);
    }
  }
  const history = useHistory();

  // Set Active For Sub Navs
  useEffect(() => {
    let a:any = String(LocalDB.getUserRole()==1 ? 'admin-area' : 'mcu-area' );
    document.body.classList.add(a);
    setActiveSubNav('/' + history.location.pathname.split('/')[1]);
    setActiveNavAdmin(getActiveName());
  },[]); // ,[history]

  useEffect(() => {
    if(showAlertBox){
      setshowAlert(true);
    }

  }, [showAlertBox])

  // Filter Parent on behalf of child match
  const getActiveName: any = () => {
    let a = 0;
    filteredNavigation.map((item: any, index: any) => {
      if (item.childern)
        item.childern.map((citem: any, cindex: any) => {
          if (citem.link == activeSubNav) {
            a = index;
          }
        });
    })
    return a;
  }
 
  const navOnClickHandler = (event: any, url: string) => {
    if(isFieldEdited){
      event.preventDefault();
      setshowAlert(true);
    }else{
      setActiveSubNav(url);
      setActiveNavMCU(url);
      setshowAlert(false);
      dispatch({
        type: RequestEmailTemplateActionsType.SetEditedFields, payload: false
      });
      return;
    }
  }

  const setshowAlertHandler = (value: boolean) => {
    setshowAlert(value);
    setActiveSubNav(currentNav);
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: false
    });
    dispatch({
      type: RequestEmailTemplateActionsType.SetShowAlertBox, payload: false
    });
  }

  const AdminMenu = () => {
    return (
      <>

        {filteredNavigation.map((item, index: any) => {
          if (item.childern && item.childern.length > 0) {
            return (
              <div key={index}>
                <div id={item.text} data-testid="sidebar-navDiv" onClick={() => {(activeNavAdmin == index)?setActiveNavAdmin(undefined):setActiveNavAdmin(index)}}>
                  <ContextAwareToggle eventKey={index} name="abc" className={`${activeNavAdmin == index ? 'active' : ''}`}>
                    <a href="#">{setIcon(item.icon)} <span>{item.text}</span></a>
                  </ContextAwareToggle>
                </div>
                <Accordion.Collapse eventKey={index} className={activeNavAdmin === index ? 'show' : ''}>
                  <div className="settings__accordion-body">
                    <nav className="settings__side-navigation">
                      <ul>
                        {
                          item.childern.map((citem, cIndex: any) => {
                            return (
                              <li key={cIndex} className={activeSubNav === citem.link ? 'active' : ''} onClick={()=>{ setCurrentNav(citem.link) }}>
                                <Link data-testid="sidebar-nav" onClick={ (event) => navOnClickHandler(event, citem.link)} to={citem.link}>{setIcon(citem.icon)} {citem.text}</Link>
                              </li>
                            );
                          })
                        }
                      </ul>
                    </nav>
                  </div>
                </Accordion.Collapse>
              </div>
            )
          }
        })
        }
      </>
    )

  }

  const MCUMenu = () => {
    return (
      <div className="settings__accordion-panel">
        <nav className="settings__side-navigation">
          <ul>

            {filteredNavigation.map((item, index: any) => {
              if (!item.childern) {
                return (
                  <li key={index} className={activeNavMCU === item.link ? 'active' : '' } onClick={()=>{ setCurrentNav(item.link) }}>
                    <Link to={item.link} onClick={ (event) => navOnClickHandler(event, item.link)}>{setIcon(item.icon)} {item.text}</Link>
                  </li>
              )}}
            )}

          </ul>
        </nav>
      </div>
    )
  }

  return (
    <aside data-testid="sideBar" className="settings__sidebar">
      <Accordion defaultActiveKey="0" className="settings__accordion">

        {LocalDB.getUserRole() === 1 && AdminMenu()}

        {LocalDB.getUserRole() === 2 && MCUMenu()}

      </Accordion>
       {showAlert && (
        <AlertBox 
           navigateUrl = {currentNav}
           hideAlert={() => {
            setshowAlert(false);
            dispatch({
              type: RequestEmailTemplateActionsType.SetShowAlertBox, payload: false
            });
           }}
           setshowAlert = {setshowAlertHandler}
        />
         )}
    </aside>
    
  );
};
