import React, { useEffect, useContext, useState } from 'react';
import { TemplateHeader } from './TemplateHeader/TemplateHeader';
import { TemplateHome } from './TemplateHome/TemplateHome';
// import { Http } from 'rainsoft-js'
import { TemplateActions } from '../../../Store/actions/TemplateActions';
import { Store } from '../../../Store/Store';
import { TemplateActionsType } from '../../../Store/reducers/TemplatesReducer';
import { divide } from 'lodash';



class TemplateItem {
  name: string = '';
  expanded: boolean = false;
  type: string = 'My Template';
  docs: DocumentItem[] | null = [
    new DocumentItem('Doc 1'),
    new DocumentItem('Doc 2'),
    new DocumentItem('Doc 3'),
  ];

  constructor(name: string) {
    this.name = name;
  }
}

class DocumentItem {
  constructor(public docName: string) { }
}


const mockTemplates = [
  new TemplateItem('New Temp 1'),
  new TemplateItem('New Temp 2'),
  new TemplateItem('New Temp 3'),
]


export const TemplateManager = () => {
  return (
    <main className="ManageTemplate-Wrap">
      <TemplateHeader />
      <TemplateHome />
    </main>
  );
};
