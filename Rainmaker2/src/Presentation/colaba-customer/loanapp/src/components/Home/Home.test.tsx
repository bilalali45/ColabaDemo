import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { fireEvent, getAllByTestId, getByTestId, render, screen, waitFor } from '@testing-library/react';
import { StoreProvider } from '../../store/store';
import { InitialStateType } from '../../store/store'
