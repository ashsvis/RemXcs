library ShellLogoff;

{ Important note about DLL memory management: ShareMem must be the
  first unit in your library's USES clause AND your project's (select
  Project-View Source) USES clause if your DLL exports any procedures or
  functions that pass strings as parameters or function results. This
  applies to all strings passed to and from your DLL--even those that
  are nested in records and classes. ShareMem is the interface unit to
  the BORLNDMM.DLL shared memory manager, which must be deployed along
  with your DLL. To avoid using BORLNDMM.DLL, pass string information
  using PChar or ShortString parameters. }

uses
  Windows,
  SysUtils,
  Classes;

{$R *.res}

procedure WindowsLogOff;
var
  ph: THandle;
  tp,prevst: TTokenPrivileges;
  rl: DWORD;
begin
  OpenProcessToken(GetCurrentProcess,
                   TOKEN_ADJUST_PRIVILEGES or TOKEN_QUERY,ph);
  LookupPrivilegeValue(nil, 'SeShutdownPrivilege', tp.Privileges[0].Luid);
  tp.PrivilegeCount := 1;
  tp.Privileges[0].Attributes := 2;
  AdjustTokenPrivileges(ph, FALSE, tp, SizeOf(prevst), prevst, rl);
  ExitWindowsEx(EWX_LOGOFF, 0);
end;

exports
  WindowsLogOff;

begin

end.
