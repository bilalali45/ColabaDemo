import { act, fireEvent, waitFor } from "@testing-library/react";

export class CommonTestMethods {
    static RenderInputwithValue = async (getByTestId, name, textValue) => {
        const input = getByTestId(name + "-input");
        await waitFor(() => {
            act(() => {
                fireEvent.change(input, { target: { value: textValue } });
            })
        });
        await waitFor(() => {
            act(() => {
                fireEvent.change(input, { target: { value: textValue } });
            })
        })
    }

    static RenderRadioandClick = async (getByTestId, name) => {
        const input = getByTestId(name + "-radio");
        await waitFor(() => {
            act(() => {
                fireEvent.click(input);
            })
        });
    }

    static RenderCheckboxandClick = async (getByTestId, name) => {
        const input = getByTestId(name + "-checkbox");
        await waitFor(() => {
            act(() => {
                fireEvent.click(input);
            })
        });
    }

    static RenderButtonandClick = async (getByTestId, testId) => {
        const input = getByTestId(testId);
        await waitFor(() => {
            act(() => {
                fireEvent.click(input);
            })
        });
    }
}