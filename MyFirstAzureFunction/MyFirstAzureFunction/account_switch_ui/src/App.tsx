import React, {useEffect, useState} from 'react';
import './App.css';

// Starter URL for API
const API_BASE_URL = 'http://localhost:7208/api/accounts'; // Get accounts url

// Helper function: gets account type name
const getAccountTypeName = (type:number) => {
    switch (type) {
        case 1:
            return 'Savings';
        case 2:
            return 'Checking';
        case 3:
            return 'Retirement';
        default:
            return 'Unknown';
    }
};


// Interface: Types for accounts
interface Account {
    AccountId: number;
    AccountType: number;
    AccountName: string;
    CurrentBalance: number;
    UserID: string;
    IsActive: boolean;
}


// Form Component: Active Account
function ActiveAccountForm({ account, onSwitch }: { account: Account; onSwitch: () => void }) {
    return (
        <div>
            <h2>Current Active Account:</h2>
            <p>{`${getAccountTypeName(account.AccountType)}: ${account.AccountName}`}</p>
            <p>Balance: ${account.CurrentBalance.toFixed(2)}</p>
            <button onClick={onSwitch}>Switch Account</button>
        </div>
    );
}

// Form Component: Switch Account
function SwitchAccountForm({
                               accounts,
                               activeAccount,
                               onSwitchAccount,
                           }: {
    accounts: Account[];
    activeAccount: Account;
    onSwitchAccount: (accountId: number) => void;
}) {
    const [selectedAccount, setSelectedAccount] = useState(activeAccount.AccountId);

    return (
        <div>
            <h2>Accounts for user {activeAccount.UserID}:</h2>
            <table>
                <tbody>
                {accounts.map((account) => (
                    <tr key={account.AccountId}>
                        <td>
                            <input
                                type="radio"
                                name="account"
                                checked={selectedAccount === account.AccountId}
                                onChange={() => setSelectedAccount(account.AccountId)}
                            />
                        </td>
                        <td>{getAccountTypeName(account.AccountType)}</td>
                        <td>{account.AccountName}</td>
                    </tr>
                ))}
                </tbody>
            </table>
            <button onClick={() => onSwitchAccount(selectedAccount)}>Switch Account</button>
        </div>
    );
}


// Main App Component
function App() {
    // Hooks
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [activeAccount, setActiveAccount] = useState<Account | null>(null);
    const [isSwitching, setIsSwitching] = useState(false);

    // Runs when the component mounts, fetches account data from server
    useEffect(() => {
        // Fetch accounts for a specific user (e.g., 10114)
        const fetchAccounts = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/10114`);
                const data = await response.json();

                setAccounts(data.Value);

                // Find active account, default to first if none is active
                const active = data.Value.find((account: Account) => account.IsActive);
                setActiveAccount(active || data.Value[0] || null); // Set to first account if none is active
            } catch (error) {
                console.error('Error fetching accounts:', error);
            }
        };
        fetchAccounts();
    }, []);

    const handleSwitchAccount = async (accountId: number) => {
        if (!activeAccount) return;
        try {
            const response = await fetch(`${API_BASE_URL}/switch`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    UserId: activeAccount.UserID,
                    AccountId: accountId,
                }),
            });
            const result = await response.json();
            console.log(result);

            // Update account data to reflect the new active account
            const updatedAccounts = accounts.map((acc) => ({
                ...acc,
                IsActive: acc.AccountId === accountId,
            }));
            setAccounts(updatedAccounts);
            const newActive = updatedAccounts.find((acc) => acc.AccountId === accountId);
            setActiveAccount(newActive || activeAccount);
            setIsSwitching(false);
        } catch (error) {
            console.error('Error switching account:', error);
        }
    };

    return (
        <div className="App">
            <main className="Main-content">
                {isSwitching ? (
                    <SwitchAccountForm
                        accounts={accounts}
                        activeAccount={activeAccount!}
                        onSwitchAccount={handleSwitchAccount}
                    />
                ) : activeAccount ? (
                    <ActiveAccountForm account={activeAccount} onSwitch={() => setIsSwitching(true)} />
                ) : (
                    <p>Loading accounts...</p>
                )}
            </main>
        </div>
    );
}

export default App;