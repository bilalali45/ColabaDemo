import React, { useEffect, useContext, useState } from 'react';
import { TemplateHeader } from './TemplateHeader/TemplateHeader';
import { TemplateHome } from './TemplateHome/TemplateHome';
// import { Http } from 'rainsoft-js'
import { TemplateActions } from '../../../Store/actions/TemplateActions';
import { Store } from '../../../Store/Store';
import { TemplateActionsType } from '../../../Store/reducers/TemplatesReducer';
import { divide } from 'lodash';

export const TemplateManager = () => {
  return (
    <main className="ManageTemplate-Wrap">
      <TemplateHeader />
      <TemplateHome />
    </main>
  );
};
