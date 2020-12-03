export enum Role {
  ADMIN_ROLE = 1,
  MCU_ROLE = 2
}

export const navigation = [
  //#region Admin Navigation
  {
    text: 'Users',
    icon: 'Users',
    role: Role.ADMIN_ROLE,
    link: '/Users',
    childern: [
      {
        text: 'Manage Users',
        icon: 'ManageUsers',
        role: Role.ADMIN_ROLE,
        //link: '/ManageUsers'
        link: '/Profile'
      },
      // {
      //   text: 'Team Roles',
      //   icon: 'TeamRoles',
      //   role: Role.ADMIN_ROLE,
      //   link: '/TeamRoles'
      // }
    ]
  },
  {
    text: 'Needs List',
    icon: 'NeedsList',
    role: Role.ADMIN_ROLE,
    link: '/NeedsList',
    childern: [
      {
        text: 'Document Templates',
        icon: 'DocumentTemplates',
        role: Role.ADMIN_ROLE,
        link: '/ManageDocumentTemplate'
      },
      {
        text: 'Request Email Templates',
        icon: 'RequestEmailTemplates',
        role: Role.ADMIN_ROLE,
        link: '/RequestEmailTemplates'
      }
    ]
  },
  {
    text: 'Integrations',
    icon: 'Integrations',
    role: Role.ADMIN_ROLE,
    link: '/Integrations',
    childern: [
      {
        text: 'Loan Origination System',
        icon: 'LoanOriginationSystem',
        role: Role.ADMIN_ROLE,
        link: '/LoanOriginationSystem'
      }
    ]
  },
  //#endregion
  //#region MCU Navigations
  {
    text: 'Profile',
    icon: 'Profile',
    role: Role.MCU_ROLE,
    link: '/Profile'
  },
  {
    text: 'Document Templates',
    icon: 'DocumentTemplates',
    role: Role.MCU_ROLE,
    link: '/ManageDocumentTemplate'
  }
  //#endregion
];
